using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.VaultService.Domain.Handlers;
using Omikron.VaultService.Infrastructure.PropertyData;
using Omikron.VaultService.Infrastructure.UkVehicleData;

namespace Omikron.VaultService.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAccountService, AccountService>();

            services.AddHttpClient<GetPropertyValueQueryHandler>((x, client) => client.BaseAddress = new Uri(x.GetRequiredService<PropertyDataSettings>().BaseUrl));
            services.AddHttpClient<GetVehicleValueQueryHandler>((x, client) => client.BaseAddress = new Uri(x.GetRequiredService<UkVehicleSettings>().BaseUrl));

            return services;
        }

        public static IServiceCollection AddThirdPartyApiServiceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UkVehicleSettings>(configuration.GetSection("ApiServices:VehicleData")).AddSingleton(x => x.GetRequiredService<IOptions<UkVehicleSettings>>().Value);
            services.Configure<PropertyDataSettings>(configuration.GetSection("ApiServices:PropertyData")).AddSingleton(x => x.GetRequiredService<IOptions<PropertyDataSettings>>().Value);

            return services;
        }
    }
}
