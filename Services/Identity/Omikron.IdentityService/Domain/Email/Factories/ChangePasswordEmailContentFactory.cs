using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Email;
using E = FluentEmail.Core.Email;

namespace Omikron.IdentityService.Domain.Email.Factories
{
    public class ChangePasswordEmailModel : BaseEmailConfirmationByTokenModel
    {
        public ChangePasswordEmailModel(User user, string token, string tenantIdentifier) : base(user, token, tenantIdentifier)
        {
        }
    }

    public class ChangePasswordEmailContentFactory : IEmailContentFactory<string, ChangePasswordEmailModel>
    {

        public ChangePasswordEmailContentFactory()
        {
        }

        public string Factory(ChangePasswordEmailModel model)
        {
            var template = GetTemplate();
            return E.DefaultRenderer.Parse(template, new
            {
                LastName = model.User.LastName,
                FirstName = model.User.FirstName
            });
        }

        private static string GetTemplate()
        {
            return "Hello, ##FirstName##. ##LastName##. <br/>Please change your password using this <a href='##Url##'>link</a>.";
        }
    }
}