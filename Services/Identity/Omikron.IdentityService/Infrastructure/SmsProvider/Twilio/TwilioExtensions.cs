using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Configuration;

namespace Omikron.IdentityService.Infrastructure.SmsProvider.Twilio
{
    public static class TwilioExtensions
    {
        public static IServiceCollection AddTwilioSmsProvider(this IServiceCollection service, IConfiguration configuration)
        {
            return service
                .Configure<TwilioConfiguration>(config: configuration.GetSection(key: ApiServices.ApiServicesTwilioConfigurationKey))
                .AddScoped<ISmsProvider, TwilioSmsProvider>();
        }
    }
}