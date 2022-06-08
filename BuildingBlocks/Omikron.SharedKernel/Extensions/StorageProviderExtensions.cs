using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Storage;

namespace Omikron.SharedKernel.Extensions
{
    public static class StorageProviderExtensions
    {
        public static IServiceCollection AddAzureStorageProvider(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>(key: "AzureBlobStorage:ConnectionString");
            var containerName = configuration.GetValue<string>(key: "AzureBlobStorage:ContainerName");
            var token = configuration.GetValue<string>(key: "AzureBlobStorage:Token");
            service.AddScoped<IStorageProvider<Blob, Uri>>(implementationFactory: provider => new AzureStorageProvider(connectionString: connectionString, containerName: containerName));
            return service;
        }
    }
}