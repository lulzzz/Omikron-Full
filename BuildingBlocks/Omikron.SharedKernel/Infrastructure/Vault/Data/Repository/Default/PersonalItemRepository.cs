using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class PersonalItemRepository : RepositoryBase<PersonalItem>, IPersonalItemRepository
	{
		private readonly VaultServiceDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public PersonalItemRepository(VaultServiceDatabaseContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

		public async Task DeletePersonalItemsByUserId(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			var personalItems = await GetPersonalItemsByOwnerId(ownerId, cancellationToken);

			if (personalItems.Any())
			{
				_dbContext.RemoveRange(personalItems);
			}
		}

		public async Task<IEnumerable<PersonalItem>> GetPersonalItemsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.PersonalItems
				.Where(p => p.OwnerId == ownerId)
				.Include(p => p.PersonalItemValues)
				.ToListAsync(cancellationToken);
		}

        public async Task<ManualAccountDetailsViewModel> GetPersonalItemWithTransactions(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.PersonalItems
                .Where(x => x.Id == accountId)
                .Include(x => x.FinancialAgreement).ThenInclude(x => x.AccountBalances)
                .Include(x => x.PersonalItemValues)
                .ProjectTo<ManualAccountDetailsViewModel>(_mapper.ConfigurationProvider )
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> RemovePersonalItemById(Guid accountId, CancellationToken cancellationToken)
        {
            var personalItem = await _dbContext.PersonalItems
            .Include(p => p.FinancialAgreement)
            .FirstOrDefaultAsync(p => p.Id == accountId, cancellationToken);

            if (personalItem == null)
            {
                return false;
            }

            if (personalItem.FinancialAgreement.IsNotNull())
            {
                personalItem.FinancialAgreement.LoanType = null;
            }

            _dbContext.PersonalItems.Remove(personalItem);

            return true;

        }

        public async Task<PersonalItemViewModel> GetPersonalItemDetails(Guid accountId, CancellationToken cancellationToken)
        {
			return await _dbContext.PersonalItems
                .Where(x => x.Id == accountId)
                .ProjectTo<PersonalItemViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
		}

        public async Task<PersonalItem> FindPersonalItemWithTransactionHistoryAndFinance(Guid accountId, CancellationToken cancellationToken)
        {
			return await _dbContext.PersonalItems
                .Include(p => p.PersonalItemValues)
                .Include(x => x.FinancialAgreement)
                .FirstOrDefaultAsync(x => x.Id == accountId, cancellationToken);
		}

        public string GetPersonalItemIdByFinanceAgreementId(Guid financeAgreementId)
        {
            return _dbContext.PersonalItems
                .Where(v => v.FinancialAgreementId == financeAgreementId)
                .Select(v => v.Id.ToString()).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
		}

		public async Task<IEnumerable<PersonalItem>> GetFilteredPersonalItems(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken)
		{
            var result = _dbContext.PersonalItems
                .Include(a => a.PersonalItemValues)
                .Where(a => a.OwnerId == customerId &&
                            !vaultEntries.Contains(a.Id));

            if (!archived)
            {
                result = result.Where(a => a.IsArchived == archived);
            }

            if (year != null)
            {
                result = result.Where(p => p.PersonalItemValues.Any(x => x.EntryDate.Year == year));
            }

            return await result
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> PersonalItemExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId)
        {
            return await _dbContext.PersonalItems.AnyAsync(account => account.OwnerId == userId && account.Id != accountId
            && account.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<PersonalItem> GetPersonalItem(Guid accountId, CancellationToken cancellationToken)
        {
            var personalItem = await _dbContext.PersonalItems
                .Include(x => x.FinancialAgreement)
                .Where(x => x.Id == accountId)
                .FirstOrDefaultAsync(cancellationToken);

            return personalItem;
        }
    }
}
