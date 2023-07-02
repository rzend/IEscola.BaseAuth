using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using IEscola.Auth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using IEscola.Auth.Repositories;
using IEscola.Auth.Services.Interfaces;
using IEscola.Core.Notificacoes;
using IEscola.Core.Notificacoes.Interfaces;

namespace IEscola.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            ConfigureAuthentication(services);

            ConfigureSwagger(services);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddHttpContextAccessor();
            services.AddSingleton<ISettings, Settings>();
            services.AddSingleton<ICredentialsRepository, CredentialsRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "IEscola Project Auth V1");
            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Projeto IEscola Auth",
                    Version = "V1",
                    Description = "Projeto IEscola Auth"
                });
            });
        }
        private void ConfigureAuthentication(IServiceCollection services)
        {
            var settings = Configuration.GetSection("Settings").Get<Settings>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = GetSigningKeys(settings),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private IEnumerable<SecurityKey> GetSigningKeys(Settings settings)
        {
            return settings.Apis.Select(a => new SymmetricSecurityKey(EncodeAscII(a.Secret)));
        }

        private static byte[] EncodeAscII(string secret)
        {
            return Encoding.ASCII.GetBytes(secret);
        }
    }
}
