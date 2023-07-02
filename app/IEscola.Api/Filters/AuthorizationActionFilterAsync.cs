using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Flurl.Http;
using IEscola.Core.Extensions;

namespace IEscola.Api.Filters
{
    public class AuthorizationActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
    {
        private const string AUTHORIZATION = "Authorization";
        private const string APIKey = "api-key";

        public AuthorizationActionFilterAsyncAttribute()
        {
            
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var _settingsService = (ISettingsService)context.HttpContext.RequestServices.GetService(typeof(ISettingsService));
            
            // execute any code before the action executes
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKey, out var extractedApiKey))
            {
                // Não encontrou
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "ApiKey não encontrada ou inválida",
                };
                return;
            }

            if (extractedApiKey != _settingsService.GetSettings().MyApiKey)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "ApiKey não encontrada ou inválida"
                };

                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue(AUTHORIZATION, out var token))
            {
                // Não encontrou
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Token inválido"
                };
                return;
            }

            if (!await TokenIsValid(token, extractedApiKey))
            {
                // Não encontrou
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Token inválido"
                };
                return;
            }

            await next();

            // execute any code after the action executes
        }

        public async Task<bool> TokenIsValid(string token, string extractedApiKey)
        {

            var flurlResponse = await @"http://localhost:3683/api/Auth/authenticated"
                .AllowAnyHttpStatus()
                .WithOAuthBearerToken(token.ToCleanToken())
                .GetAsync();


            return flurlResponse.StatusCode >= 200 && flurlResponse.StatusCode < 300;
        }
    }
}
