using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly OmikronIdentityDbContext _identityDbContext;

        public PhoneNumberRepository(OmikronIdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<Guid> AddPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            await _identityDbContext.PhoneNumbers.AddAsync(entity: phoneNumber, cancellationToken: cancellationToken);
            return phoneNumber.Id;
        }

        public async Task DeleteNumber(string number, CancellationToken cancellationToken = default)
        {
            var phoneNumber = await _identityDbContext.PhoneNumbers.FirstOrDefaultAsync(n => n.Number == number, cancellationToken);

            if (phoneNumber == null)
            {
                return;
            }

            _identityDbContext.PhoneNumbers.Remove(phoneNumber);
        }

        public async Task<PhoneNumber> GetPhoneNumberByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.PhoneNumbers
                .Include(p => p.Owner)
                .Where(p => p.Owner.ExternalId == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PhoneNumber> GetPhoneNumberByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.PhoneNumbers
                .Include(p => p.Owner)
                .Where(p => p.Owner.UserName == email)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PhoneNumber> GetPhoneNumberByIdAsync(Guid phoneNumberId, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.PhoneNumbers.FirstOrDefaultAsync(predicate: p => p.Id == phoneNumberId, cancellationToken: cancellationToken);
        }

        public async Task<PhoneNumber> GetPhoneNumberByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.PhoneNumbers.Include(p => p.Owner)
                                                        .OrderByDescending(p => p.CreatedAt)
                                                        .FirstOrDefaultAsync(predicate: p => p.Number == number, cancellationToken: cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _identityDbContext.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            _identityDbContext.Update(entity: phoneNumber);
        }
    }
}