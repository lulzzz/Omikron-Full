using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Omikron.SharedKernel.Extensions;

namespace Omikron.IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args: args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args: args)
                .ConfigureAppConfiguration(configureDelegate: Configuration)
                .ConfigureWebHostDefaults(configure: webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static void Configuration(HostBuilderContext context, IConfigurationBuilder config)
        {
            var configBuilder = config.SetBasePath(basePath: Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);


            if (!context.HostingEnvironment.IsLocal())
            {
                // secrets/appsettings.secrets.json is added to aks as a kubernetes secret and used to store the creds needed to access keyvault
                configBuilder.AddJsonFile(path: "secrets/appsettings.secrets.json", optional: true, reloadOnChange: true);
                config.AddAzureKeyVaultConfigs();
            }
        }
    }
}