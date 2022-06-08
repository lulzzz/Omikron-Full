using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataRepository(this IServiceCollection serviceCollection, Assembly [] assemblies)
        {
            const string marker = "Repository";
            return serviceCollection.AddServicesAsImplementedInterface(marker, ServiceLifetime.Scoped, assemblies, i => i.Name.EndsWith(marker));
        }

        public static IServiceCollection AddDataRepository(this IServiceCollection serviceCollection)
        {
            return AddDataRepository(serviceCollection, new[] {Assembly.GetExecutingAssembly()});
        }
    }
}