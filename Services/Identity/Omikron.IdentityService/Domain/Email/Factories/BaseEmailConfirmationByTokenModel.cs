using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Domain.Email.Factories
{
    public abstract class BaseEmailConfirmationByTokenModel
    {
        public string TenantIdentifier { get; }
        public User User { get; }
        public string Token { get; }

        protected BaseEmailConfirmationByTokenModel(User user, string token, string tenantIdentifier)
        {
            User = user;
            Token = token;
            TenantIdentifier = tenantIdentifier;
        }
    }
}