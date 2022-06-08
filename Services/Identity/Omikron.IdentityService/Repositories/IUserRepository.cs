using System;
using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByConfirmToken(string token, CancellationToken cancellationToken = default);
        Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default);
        Task<User> GetUserByIdIncludePhoneNumberAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<User> GetUserByUsernameIncludePhoneNumberAsync(string username, CancellationToken cancellationToken = default);
    }
}