using System;
using System.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.ServiceRegistirations
{
    public static class ServiceRegistirations
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {

            services.AddDbContext<EntityDbContext>(options => options.UseMySql(connectionString : Configurations.connectionString, new MySqlServerVersion(new Version(7,0,0))));

        }
    }
}