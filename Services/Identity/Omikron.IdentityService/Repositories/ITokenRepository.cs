using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Repositories
{
    public interface ITokenRepository
    {
        Task<ConfirmationToken> GetConfirmationTokenByUserId(int userId, CancellationToken cancellationToken = default);
    }
}