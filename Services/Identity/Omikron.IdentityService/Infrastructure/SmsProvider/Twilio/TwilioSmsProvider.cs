using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Omikron.SharedKernel.Infrastructure.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Omikron.IdentityService.Infrastructure.SmsProvider.Twilio
{
    public class TwilioSmsProvider : ISmsProvider
    {
        private readonly TwilioConfiguration _configuration;

        public TwilioSmsProvider(IOptionsMonitor<TwilioConfiguration> options)
        {
            _configuration = options.CurrentValue;
        }

        public async Task SendSms(string toPhoneNumber, string body)
        {
            TwilioClient.Init(username: _configuration.AccountSid, password: _configuration.AuthToken);

            await MessageResource.CreateAsync(
                body: body,
                from: "Omikron",
                to: new PhoneNumber(number: toPhoneNumber));
        }
    }
}