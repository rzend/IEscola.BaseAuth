﻿using IEscola.Api;
using IEscola.Api.Filters;
using IEscola.Application.Interfaces;
using IEscola.Application.Services;
using IEscola.Core.Notificacoes;
using IEscola.Core.Notificacoes.Interfaces;
using IEscola.Domain.Interfaces;
using IEscola.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IEscola.CrossCutting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIEscolaServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Container de DI
            services.AddHttpContextAccessor();
            var settings = configuration.GetSection("Settings").Get<Settings>();
            services.Configure<Settings>(configuration.GetSection("Settings"));

            services.AddSingleton<ISettings, Settings>();

            services.AddScoped<ISettingsService, SettingsService>();

            // Services
            services.AddScoped<IDisciplinaService, DisciplinaService>();


            // Repositories
            services.AddSingleton<IDisciplinaRepository, DisciplinaRepository>();


            // Outros objetos
            services.AddScoped<INotificador, Notificador>();


            // ActionFilter
            services.AddScoped<AuthorizationActionFilterAsyncAttribute>();

            // Vida útil dos objetos na memória -> Quando a aplicação "subir"

            // services.AddSingleton -> Instancia única na memória()
            // services.AddScoped -> Instancia única na memória() durante a requisição
            // services.AddTransient -> Uma instancia nova por chamada(E não request)


            // Alerta Singleton não pode ter dependencia para Scoped
            return services;
        }
    }
}
