using Microsoft.Extensions.DependencyInjection;
using Omikron.IdentityService.Domain.Services;
using Omikron.TenantService.Domain.Services;

namespace Omikron.IdentityService.Infrastructure.Extensions
{
    public static class FactoriesExtensions
    {
        public static IServiceCollection AddFactoriesAndServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserAccountService, UserAccountService>()
                .AddScoped<ILocationLookupService, LocationLookupService>();
        }
    }
}