using System.Threading.Tasks;

namespace Omikron.IdentityService.Infrastructure.SmsProvider
{
    public class NullSmsProvider : ISmsProvider
    {
        public async Task SendSms(string toPhoneNumber, string body)
        {
        }
    }
}
