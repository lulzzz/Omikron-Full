using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using E = FluentEmail.Core.Email;

namespace Omikron.IdentityService.Domain.Email.Factories
{
    public class ConfirmationEmailByTokenModel : BaseEmailConfirmationByTokenModel
    {
        public ConfirmationEmailByTokenModel(User user, string token, string tenantIdentifier) : base(user, token, tenantIdentifier)
        {
        }
    }

    public class ConfirmationEmailByTokenContentFactory : IEmailContentFactory<string, ConfirmationEmailByTokenModel>
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantAccessor _tenantAssessor;

        public ConfirmationEmailByTokenContentFactory(IConfiguration configuration, ITenantAccessor tenantAssessor)
        {
            _configuration = configuration;
            _tenantAssessor = tenantAssessor;
        }

        public string Factory(ConfirmationEmailByTokenModel model)
        {
            var webappUrl = _configuration.GetValue<string>("WebApplication:ConfirmationEmailUri");
            var tenant = _tenantAssessor.GetTenant(model.TenantIdentifier);

            var url = string.Format(webappUrl, tenant.Identifier, model.Token);

            var template = GetTemplate();
            return E.DefaultRenderer.Parse(template, new
            {
                TenantIdentifier = tenant.Identifier,
                TenantName = tenant.Name,
                LastName = model.User.LastName,
                FirstName = model.User.FirstName,
                Url = url
            });
        }

        //TODO: The plan is to pull templates from DB but we will back to this once when we proceed with Notification Service
        private static string GetTemplate()
        {
            return "Hello, ##FirstName##. ##LastName##. <br/>Please confirm your email following this <a href='##Url##'>link</a>.";
        }
    }
}