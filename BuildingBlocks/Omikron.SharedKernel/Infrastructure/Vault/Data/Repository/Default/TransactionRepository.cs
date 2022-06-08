using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
	public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
	{
		private readonly VaultServiceDatabaseContext _dbContext;

		public TransactionRepository(VaultServiceDatabaseContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Transaction>> SearchTransactionsByAccountId(Guid accountId, string searchTerm = "", CancellationToken cancellationToken = default)
		{
			var result = searchTerm.IsNull() ?
				_dbContext.Transactions.Include(t => t.Merchant).Where(t => t.AccountId == accountId) :
				_dbContext.Transactions.Include(t => t.Merchant).Where(t => t.AccountId == accountId && (t.Merchant.DisplayName.Contains(searchTerm) || t.Category.Contains(searchTerm) || t.TransactionInformation.Contains(searchTerm)));

			return await result.ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<Transaction>> GetAllUserTransactions(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Transactions
				.Include(t => t.Account)
				.Where(t => t.Account.OwnerId == ownerId)
				.ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<TransactionsPerCategoryCountContainer>> GetNumberOfTransactionsPerCategoryAsync(CustomerId ownerId, DateRange dateFilter, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Transactions
				.Include(t => t.Account)
				.Where(t => t.Account.OwnerId == ownerId && t.Date >= dateFilter.From && t.Date <= dateFilter.To)
				.GroupBy(t => t.Category)
				.Select(t => new TransactionsPerCategoryCountContainer()
				{
					Category = t.Key,
					NumberOfIncomeTransactions = t.Where(x => x.CreditDebitIndicator == CreditDebitIndicator.Credit).Count(),
					NumberOfSpendingTransactions = t.Where(x => x.CreditDebitIndicator == CreditDebitIndicator.Debit).Count(),
				})
				.ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<CategoryData>> GetFilteredCategories(CustomerId customerId, DateRange dateFilter, IEnumerable<AccountType> assetLiabilityTypes, IEnumerable<Guid> vaultEntries, IEnumerable<string> categories, CancellationToken cancellationToken)
		{
			return await _dbContext.Transactions
				.Include(t => t.Account)
				.Where(t => t.Account.OwnerId == customerId &&
							t.Date >= dateFilter.From &&
							t.Date <= dateFilter.To &&
							!assetLiabilityTypes.Contains(t.Account.Type) &&
							!vaultEntries.Contains(t.Account.Id) &&
							!categories.Contains(t.Category))
				.GroupBy(t => t.Category)
				.Select(g => new CategoryData()
				{
					CategoryName = g.Key,
					CreditAmount = g.Sum(t => t.CreditDebitIndicator == CreditDebitIndicator.Credit ? t.Amount : 0),
					DebitAmount = g.Sum(t => t.CreditDebitIndicator == CreditDebitIndicator.Debit ? t.Amount : 0),
					NumberOfCreditTransactions = g.Count(t => t.CreditDebitIndicator == CreditDebitIndicator.Credit),
					NumberOfDebitTransactions = g.Count(t => t.CreditDebitIndicator == CreditDebitIndicator.Debit),
				})
				.OrderByDescending(c => c.DebitAmount)
				.ThenByDescending(c => c.CreditAmount)
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<Transaction> GetFirstTransactionByDate(CustomerId userId, CancellationToken cancellationToken)
		{
			return await _dbContext.Transactions
				.Include(t => t.Account)
				.Where(t => t.Account.OwnerId == userId)
				.OrderBy(t => t.Date)
				.FirstOrDefaultAsync(cancellationToken);
		}
	}
}
