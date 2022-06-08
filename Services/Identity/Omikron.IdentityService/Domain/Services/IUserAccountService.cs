using System.Threading.Tasks;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.TenantService.Domain.Services
{
    public interface IUserAccountService
    {
        Task SendResetPasswordTokenEmailAsync(User user, string token, string tenantIdentifier);
        Task SendConfirmationEmailAsync(User user, string token, string tenantIdentifier);
        Task SendNewUserEmailAsync(User user);
    }
}