using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.IdentityService.UnitTest.Factories
{
    public class TestTenantUserManagerFactory : ITenantFactory<IdentityUserManager>
    {
        private readonly IdentityUserManager _userManager;

        public TestTenantUserManagerFactory(IdentityUserManager userManager)
        {
            _userManager = userManager;
        }

        public IdentityUserManager Factory(Tenant tenant)
        {
            return _userManager;
        }
    }
}