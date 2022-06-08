using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.SecureVault;
using Omikron.SharedKernel.Infrastructure.SecureVault.Providers;

namespace Omikron.SharedKernel.Extensions
{
    public static class SecureVaultExtensions
    {
        public static IServiceCollection AddAzureKeyVault(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<ISecureVaultProvider, AzureKeyVaultProvider>();
        }
    }
}