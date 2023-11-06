using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeTag.Persistence.Context;
namespace TimeTag.Api.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope=host.Services.CreateScope( ))
            {
                 using (var EntityDbContext= scope.ServiceProvider.GetRequiredService<EntityDbContext>())
                 {
                      try
                      {
                        EntityDbContext.Database.Migrate();
                    }
                      catch (System.Exception)
                      {
                          
                          throw;
                      }
                 }
            }
            return host;
        }
    }
}