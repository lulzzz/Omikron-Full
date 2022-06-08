using System.Diagnostics;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Email.Factories;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Email;
using Omikron.SharedKernel.Infrastructure.Logging;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Utils;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Omikron.TenantService.Domain.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IEmailService _emailService;
        private readonly ISendGridClient _sendGridClient;
        private readonly IEmailContentFactory<string, ChangePasswordEmailModel> _changePasswordContentFactory;
        private readonly IEmailContentFactory<string, ConfirmationEmailByTokenModel> _confirmationEmailContentFactory;
        private readonly ISendGridMessageFactory<NewUserEmailModel> _newUserSendGridFactory;
        private readonly LoggerContext _loggerContext;

        public UserAccountService(IEmailService emailService, ISendGridClient sendGridClient,
            IEmailContentFactory<string, ChangePasswordEmailModel> changePasswordContentFactory,
            IEmailContentFactory<string, ConfirmationEmailByTokenModel> confirmationEmailContentFactory,
            ISendGridMessageFactory<NewUserEmailModel> newUserSendGridFactory, LoggerContext loggerContext)
        {
            _emailService = emailService;
            _sendGridClient = sendGridClient;
            _changePasswordContentFactory = changePasswordContentFactory;
            _confirmationEmailContentFactory = confirmationEmailContentFactory;
            _newUserSendGridFactory = newUserSendGridFactory;
            _loggerContext = loggerContext;
        }

        public async Task SendResetPasswordTokenEmailAsync(User user, string token, string tenantIdentifier)
        {
            var content = _changePasswordContentFactory.Factory(new ChangePasswordEmailModel(user, token, tenantIdentifier));
            await _emailService.SendEmailAsync(new EmailMessage(user.Email, "Request for Password Reset - Omikron", content));
        }

        public async Task SendConfirmationEmailAsync(User user, string token, string tenantIdentifier)
        {
            var content = _confirmationEmailContentFactory.Factory(new ConfirmationEmailByTokenModel(user, token, tenantIdentifier));
            await _emailService.SendEmailAsync(new EmailMessage(user.Email, "Confirm your email - Omikron", content));
        }

        public async Task SendNewUserEmailAsync(User user)
        {
            var message = _newUserSendGridFactory.Factory(new NewUserEmailModel(user));
            var result = await _sendGridClient.SendEmailAsync(message);
            if (!result.IsSuccessStatusCode)
            {
                _loggerContext.UsageLogger.Error($"Failed to send welcome email to {user.Email} at time {Clock.GetTime()} with status code {result.StatusCode}");
            }
        }
    }
}