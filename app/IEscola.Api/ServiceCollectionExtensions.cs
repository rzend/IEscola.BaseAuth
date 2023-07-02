using IEscola.Api;
using IEscola.Api.Filters;
using IEscola.Core.Notificacoes;
using IEscola.Core.Notificacoes.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IEscola.CrossCutting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIEscolaServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            var settings = configuration.GetSection("Settings").Get<Settings>(); // Caso precise utilizar
            services.Configure<Settings>(configuration.GetSection("Settings"));
            services.AddSingleton<ISettings, Settings>();

            services.AddScoped<ISettingsService, SettingsService>();

            // ActionFilter
            services.AddScoped<AuthorizationActionFilterAsyncAttribute>();

            services.AddScoped<INotificador, Notificador>();
            

            return services;
        }
    }
}
