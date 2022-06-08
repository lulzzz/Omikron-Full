using Omikron.SharedKernel.Domain;
using SendGrid.Helpers.Mail;

namespace Omikron.SharedKernel.Infrastructure.Email
{
    public interface ISendGridMessageFactory<in TParams> : IFactory<SendGridMessage, TParams>
    {
    }
}