using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Email;

namespace Omikron.IdentityService.UnitTest.Services
{
    public class FakeEmailService : IEmailService
    {
        public Task SendEmailAsync(EmailMessage message)
        {
            return Task.CompletedTask;
        }
    }
}