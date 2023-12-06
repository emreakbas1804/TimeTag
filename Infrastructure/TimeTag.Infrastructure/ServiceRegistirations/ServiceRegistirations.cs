using System;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTag.Application.Abstractions;
using TimeTag.Infrastructure.Concretes;


namespace TimeTag.Persistence.ServiceRegistirations
{
    public static class ServiceRegistirations
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }
    }
}