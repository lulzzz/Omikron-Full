using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Extensions;

namespace Omikron.IdentityService.UnitTest.Email
{
    public class EmailContentFactoryFixture
    {
        public EmailContentFactoryFixture()
        {
            var serviceProvider = ConfigureServiceProvider();
        }

        private static IServiceProvider ConfigureServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            serviceCollection.AddEmailService(configuration);

            return serviceCollection.BuildServiceProvider();
        }
    }
}