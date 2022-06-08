using Omikron.IdentityService.Infrastructure.Data.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Repositories
{
    public interface IPhoneNumberRepository
    {
        Task<Guid> AddPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<PhoneNumber> GetPhoneNumberByIdAsync(Guid phoneNumberId, CancellationToken cancellationToken = default);
        Task<PhoneNumber> GetPhoneNumberByNumberAsync(string number, CancellationToken cancellationToken = default);
        Task<PhoneNumber> GetPhoneNumberByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<PhoneNumber> GetPhoneNumberByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        void UpdatePhoneNumber(PhoneNumber phoneNumber);
        Task DeleteNumber(string number, CancellationToken cancellationToken = default);
    }
}