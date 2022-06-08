using System;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class InvestmentRepository : RepositoryBase<Investment>, IInvestmentRepository
    {
        private readonly VaultServiceDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public InvestmentRepository(VaultServiceDatabaseContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Investments
                .Where(i => i.OwnerId == ownerId)
                .Include(i => i.InvestmentValues)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteInvestmentsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
        {
            var investments = await GetInvestmentsByOwnerId(ownerId, cancellationToken);

            if (investments.Any())
            {
                _dbContext.RemoveRange(investments);
            }
        }

        public async Task<bool> DeleteInvestmentById(Guid accountId, CancellationToken cancellationToken)
        {
            var investment = await _dbContext.Investments.FindAsync(new object[] { accountId }, cancellationToken);

            if (investment == null)
            {
                return false;
            }

            _dbContext.Investments.Remove(investment);

            return true;

        }

        public async Task<ManualAccountDetailsViewModel> GetInvestmentWithTransactionHistory(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Investments
                .Include(x => x.InvestmentValues)
                .Where(x => x.Id == accountId)
                .ProjectTo<ManualAccountDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<InvestmentViewModel> GetInvestmentDetails(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Investments
                .Where(x => x.Id == accountId)
                .ProjectTo<InvestmentViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Investment> GetInvestment(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Investments.FindAsync(new object[] { accountId }, cancellationToken: cancellationToken);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

		public async Task<IEnumerable<Investment>> GetFilteredInvestments(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken)
		{
            var result = _dbContext.Investments
                .Include(a => a.InvestmentValues)
                .Where(a => a.OwnerId == customerId &&
                            !vaultEntries.Contains(a.Id));

            if (!archived)
            {
                result = result.Where(a => a.IsArchived == archived);
            }

            if (year != null)
            {
                result = result.Where(p => p.InvestmentValues.Any(x => x.EntryDate.Year == year));
            }

            return await result
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> InvestmentExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId)
        {
            return await _dbContext.Investments.AnyAsync(account => account.OwnerId == userId && account.Id != accountId
            && account.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
