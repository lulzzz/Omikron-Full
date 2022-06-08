using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.DataTrace;
using Omikron.SharedKernel.Infrastructure.DataTrace.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class DataTraceExtensions
    {
        public static IServiceCollection AddAzureTableStorageDataTraceManager(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<IDataTraceManager<AzureTableStorageDataTraceQuery>>(provider =>
                new AzureTableStorageDataTraceManager(configuration, provider.GetService<ITenantAccessor>()));
        }
    }
}
