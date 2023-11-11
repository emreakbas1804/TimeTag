using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTag.Application.Abstractions;
using TimeTag.Persistence.Concretes;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.ServiceRegistirations
{
    public static class ServiceRegistirations
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {

            services.AddDbContext<EntityDbContext>(options => options.UseMySql(connectionString : Configurations.connectionString, new MySqlServerVersion(new Version(7,0,0))));
                       
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}