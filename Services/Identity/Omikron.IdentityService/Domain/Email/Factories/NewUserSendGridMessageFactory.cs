using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Email;
using SendGrid.Helpers.Mail;

namespace Omikron.IdentityService.Domain.Email.Factories
{
	public class NewUserEmailModel
    {
        public User User { get; }

        public NewUserEmailModel(User user)
        {
            User = user;
        }
    }

    public class NewUserSendGridMessageFactory : ISendGridMessageFactory<NewUserEmailModel>
    {
        private readonly EmailSettings _emailSettings;

        public NewUserSendGridMessageFactory(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public SendGridMessage Factory(NewUserEmailModel @params)
        {
            var message = new SendGridMessage();
            message.SetTemplateId(_emailSettings.NewUserTemplateId);
            message.SetFrom(_emailSettings.Sender, _emailSettings.SenderName);
            message.AddTo(@params.User.Email, @params.User.FirstName);

            message.SetTemplateData(new {first_name = @params.User.FirstName});
            
            return message;
        }
    }
}