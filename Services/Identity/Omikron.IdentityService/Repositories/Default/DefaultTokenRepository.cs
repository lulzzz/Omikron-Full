using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Repositories.Default
{
    public class DefaultTokenRepository : ITokenRepository
    {
        private readonly OmikronIdentityDbContext _identityDbContext;

        public DefaultTokenRepository(OmikronIdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<ConfirmationToken> GetConfirmationTokenByUserId(int userId,
            CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.ConfirmationTokens
                .FirstOrDefaultAsync(predicate: x => x.UserId == userId && x.Type == ConfirmationTokenType.ConfirmationEmail,
                    cancellationToken: cancellationToken);
        }
    }
}