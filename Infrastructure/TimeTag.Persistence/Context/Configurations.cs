using System.IO;
using Microsoft.Extensions.Configuration;

namespace TimeTag.Persistence.Context
{
    public static class Configurations
    {        
        public static string SecretKey {get {
             var builder = new ConfigurationBuilder()
                            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/TimeTag.Api"))
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
            return configuration["Secret"];
        }}
    }
}