using Microsoft.Extensions.Configuration;

namespace Omikron.SharedKernel.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAzureKeyVaultConfigs(this IConfigurationBuilder builder)
        {
            var temporaryConfig = builder.Build();
            return builder.AddAzureKeyVault(temporaryConfig["Azure:KeyVaultPath"],
                temporaryConfig["Azure:AdAppRegistrationId"],
                temporaryConfig["Azure:AdAppRegistrationSecret"]);
        }
    }
}