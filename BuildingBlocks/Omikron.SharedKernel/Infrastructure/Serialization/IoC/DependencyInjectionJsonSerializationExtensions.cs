using Omikron.SharedKernel.Infrastructure.Serialization.IoC.Microsoft;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Infrastructure.Serialization.IoC
{
    public static class DependencyInjectionJsonSerializationExtensions
    {
        /// <summary>
        ///     Register JSON Serialization. Please use chain in order to specify JSON serialization provider.
        /// </summary>
        /// <param name="serviceCollection">The current IoC service collection.</param>
        /// <returns>The chain in order to specify JSON serialization provider</returns>
        public static IDependencyInjectionJsonSerializationSelector AddJsonSerialization(this IServiceCollection serviceCollection)
        {
            return new MicrosoftDependencyInjectionJsonSerializationSelector(serviceCollection: serviceCollection);
        }
    }
}