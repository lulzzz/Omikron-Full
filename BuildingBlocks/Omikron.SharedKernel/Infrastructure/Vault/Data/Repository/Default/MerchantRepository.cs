using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
	public class MerchantRepository : RepositoryBase<Merchant>, IMerchantRepository
	{
		private readonly VaultServiceDatabaseContext _dbContext;

		public MerchantRepository(VaultServiceDatabaseContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Merchant>> GetAll(CancellationToken cancellationToken = default)
		{
			return await FindAll().ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<MerchantViewModel>> GetFilteredMerchants(CustomerId customerId, DateRange dateFilter, int page, int pageSize, IEnumerable<AccountType> assetLiabilityTypes, IEnumerable<Guid> vaultEntries, IEnumerable<string> categories, CancellationToken cancellationToken)
		{
			return await _dbContext.Transactions
				.Include(t => t.Account)
				.Include(t => t.Merchant)
				.Where(t => t.Account.OwnerId == customerId &&
							t.Merchant != null &&
							t.Date >= dateFilter.From &&
							t.Date <= dateFilter.To &&
							t.CreditDebitIndicator == CreditDebitIndicator.Debit &&
							!assetLiabilityTypes.Contains(t.Account.Type) &&
							!vaultEntries.Contains(t.Account.Id) &&
						    !categories.Contains(t.Category))
				.Select(t => new MerchantViewModel()
				{
					Logo = t.Merchant.Logo,
					Currency = Constants.DefaultCurrencyCode,
					Amount = t.Merchant.Transactions.Where(t => t.Date >= dateFilter.From &&
														   t.Date <= dateFilter.To && 
														   t.Account.OwnerId == customerId).Sum(t => t.Amount),
					Transactions = t.Merchant.Transactions.Where(t => t.Date >= dateFilter.From &&
														   t.Date <= dateFilter.To &&
														   t.Account.OwnerId == customerId).Count()
				})
				.Distinct()
				.OrderByDescending(t => t.Amount)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<MerchantContainerViewModel> GetMerchantData(CustomerId customerId, DateRange dateFilter, CancellationToken cancellationToken)
		{
			var container = await _dbContext.Transactions
				.Include(t => t.Account)
				.Include(t => t.Merchant)
				.Where(t => t.Account.OwnerId == customerId && 
							t.CreditDebitIndicator == CreditDebitIndicator.Debit &&
							t.Date >= dateFilter.From &&
							t.Date <= dateFilter.To &&
							t.Merchant.Name != null)
				.Select(t => new 
				{
					Amount = t.Merchant.Transactions.Where(t => t.Date >= dateFilter.From &&
														   t.Date <= dateFilter.To &&
														   t.Account.OwnerId == customerId).Sum(t => t.Amount),
                    t.Merchant.Name
				}).Distinct().ToListAsync(cancellationToken);

			return new MerchantContainerViewModel()
			{
				Currency = Constants.DefaultCurrencyCode,
				TotalValue = container.Sum(t => t.Amount),
				NumberOfMerchants = container.Select(t => t.Name).Count()
            };
		}

		public async Task<IEnumerable<Merchant>> GetMerchantsByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Merchants
				.Where(m => names.Contains(m.Name))
				.ToListAsync(cancellationToken);
		}
	}
}
