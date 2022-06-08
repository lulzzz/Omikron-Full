using Omikron.IdentityService.Domain.Email.Factories;
using Omikron.SharedKernel.Infrastructure.Email;

namespace Omikron.IdentityService.UnitTest.Factories
{
    public class FakeChangePasswordEmailContentFactory : IEmailContentFactory<string, ChangePasswordEmailModel>
    {
        public string Factory(ChangePasswordEmailModel model)
        {
            return string.Empty;
        }
    }
}