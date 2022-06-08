using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}