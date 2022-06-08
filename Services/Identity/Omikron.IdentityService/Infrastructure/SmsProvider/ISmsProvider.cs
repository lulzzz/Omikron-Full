using System.Threading.Tasks;

namespace Omikron.IdentityService.Infrastructure.SmsProvider
{
    public interface ISmsProvider
    {
        Task SendSms(string toPhoneNumber, string body);
    }
}