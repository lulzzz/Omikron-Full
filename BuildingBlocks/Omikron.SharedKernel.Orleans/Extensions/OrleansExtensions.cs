using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Orleans.Providers;

namespace Omikron.SharedKernel.Orleans.Extensions
{
    public static class OrleansExtensions
    {
        public static IServiceCollection AddOrleansGrainProvider(this IServiceCollection services)
        {
            return services.AddScoped<IGrainProvider, OrleansGrainProvider>();
        }
    }
}