using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Repositories.Default
{
    public class DefaultUserRepository : IUserRepository
    {
        private readonly OmikronIdentityDbContext _identityDbContext;

        public DefaultUserRepository(OmikronIdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<User> GetUserByConfirmToken(string token, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.ConfirmationTokens
                .Include(navigationPropertyPath: x => x.User)
                .Where(predicate: x => x.Value.Equals(token) && x.Type == ConfirmationTokenType.ConfirmationEmail)
                .Select(selector: x => x.User)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Users
                .Where(predicate: x => x.ExternalId == userId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetUserByIdIncludePhoneNumberAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Users
                .Include(x => x.PhoneNumberForVerification)
                .Where(x => x.ExternalId == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetUserByUsernameIncludePhoneNumberAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.Users
                .Include(x => x.PhoneNumberForVerification)
                .Where(x => x.UserName == username)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}