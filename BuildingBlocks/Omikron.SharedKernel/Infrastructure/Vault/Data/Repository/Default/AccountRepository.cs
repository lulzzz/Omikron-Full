using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
	public class AccountRepository : RepositoryBase<Account>, IAccountRepository
	{
		private readonly VaultServiceDatabaseContext _dbContext;
		private readonly IMapper _mapper;

		public AccountRepository(VaultServiceDatabaseContext dbContext, IMapper mapper) : base(dbContext)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<Account>> GetAccountsWithBalanceByOwnerId(Guid id, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Accounts
				.Include(a => a.AccountBalances)
				.Include(a => a.PersonalItems)
				.Include(a => a.Properties)
				.Include(a => a.Vehicles)
				.Where(a => a.OwnerId == id.ToString())
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<Account> GetUserAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Accounts
				.Include(a => a.AccountBalances)
				.AsNoTracking()
				.Where(a => a.ExternalId == accountId)
				.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IEnumerable<Account>> GetAccountsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Accounts
				.Where(a => a.OwnerId == ownerId)
				.Include(a => a.AccountBalances)
				.Include(a => a.Transactions)
				.ToListAsync(cancellationToken);
		}

		public async Task DeleteAccountsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			var accounts = await GetAccountsByOwnerId(ownerId, cancellationToken);

			if (accounts.Any())
			{
				_dbContext.Accounts.RemoveRange(accounts);
			}
		}

		public async Task<bool> DeleteAccountByExternalId(Guid externalId, CancellationToken cancellationToken)
		{
			var account = await _dbContext.Accounts
				.FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);

			if (account == null)
			{
				return false;
			}

			_dbContext.Accounts.Remove(account);

			return true;

		}
		public async Task<MortgageManualAccountViewModel> GetPropertyMortgageAccount(Guid financeId, Guid accountId, CancellationToken cancellationToken)
		{
			var account = await _dbContext.Accounts
				.AsNoTracking()
				.Include(x => x.Transactions)
				.Include(x => x.AccountBalances)
				.Include(x => x.Properties)
				.Where(x => x.Id == financeId)
				.ProjectTo<MortgageManualAccountViewModel>(_mapper.ConfigurationProvider, new { AccountId = accountId })
				.FirstOrDefaultAsync(cancellationToken);

			return account;
		}

		public async Task<VehicleFinanceViewModel> GetVehicleFinanceAccount(Guid financeId, Guid accountId, CancellationToken cancellationToken)
		{
			var account = await _dbContext.Accounts
				.AsNoTracking()
				.Include(x => x.Transactions)
				.Include(x => x.AccountBalances)
				.Include(x => x.Vehicles)
				.Where(x => x.Id == financeId)
				.ProjectTo<VehicleFinanceViewModel>(_mapper.ConfigurationProvider, new { AccountId = accountId })
				.FirstOrDefaultAsync(cancellationToken);

			return account;
		}

		public async Task<bool> DeleteMortgage(Guid accountId, CancellationToken cancellationToken)
		{
			var mortgage = await _dbContext.Accounts.FindAsync(new object[] { accountId }, cancellationToken);

			if (mortgage == null)
			{
				return false;
			}

			_dbContext.Accounts.Remove(mortgage);

			return true;
		}

		public async Task<bool> DeleteVehicleFinance(Guid accountId, CancellationToken cancellationToken)
		{
			var vehicleFinance = await _dbContext.Accounts.FindAsync(new object[] { accountId }, cancellationToken);
			if (vehicleFinance == null) return false;

			_dbContext.Accounts.Remove(vehicleFinance);

			return true;
		}

		public async Task<PersonalItemFinanceViewModel> GetPersonalItemFinanceAccount(Guid financeId, Guid accountId, CancellationToken cancellationToken)
		{
			var account = await _dbContext.Accounts
				.AsNoTracking()
				.Include(x => x.Transactions)
				.Include(x => x.AccountBalances)
				.Include(x => x.PersonalItems)
				.Where(x => x.Id == financeId)
				.ProjectTo<PersonalItemFinanceViewModel>(_mapper.ConfigurationProvider, new { AccountId = accountId })
				.FirstOrDefaultAsync(cancellationToken);

			return account;
		}

		public async Task<bool> DeletePersonalItemFinance(Guid accountId, CancellationToken cancellationToken)
		{
			var personalFinance = await _dbContext.Accounts.FindAsync(new object[] { accountId }, cancellationToken);

			if (personalFinance == null)
			{
				return false;
			}

			_dbContext.Accounts.Remove(personalFinance);

			return true;
		}

		public async Task<ManualAccountDetailsViewModel> GetManualAccountDetails(Guid accountId, CancellationToken cancellationToken)
		{
			return await _dbContext.Accounts
				.Where(a => a.ExternalId == accountId)
				.ProjectTo<ManualAccountDetailsViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<Account> GetAccount(Guid accountId, CancellationToken cancellationToken)
		{
			return await _dbContext.Accounts.FirstOrDefaultAsync(x => x.ExternalId == accountId, cancellationToken);
		}

		public async Task<ManualAccountViewModel> GetManualAccount(Guid accountId, CancellationToken cancellationToken)
		{
			return await _dbContext.Accounts
				.Where(x => x.ExternalId == accountId)
				.ProjectTo<ManualAccountViewModel>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
		}

		public async Task<IEnumerable<Account>> GetUserAccountsByProvider(CustomerId ownerId, string provider, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Accounts
				.Where(a => a.OwnerId == ownerId && a.Provider == provider)
				.ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<Account>> SearchLoans(CustomerId ownerId, string search, CancellationToken cancellationToken = default)
		{
			//TODO: I left one-to-may relationship between Personal Items and Vehicles because of lack of time.
			//It should be one-to-one relationship but neither side should be dependent
			return search.IsNullOrEmpty() ?
				await _dbContext.Accounts.Where(a => a.OwnerId == ownerId &&
													 a.Type == AccountType.Loan &&
													 a.LoanType != LoanType.Mortgage &&
													!a.PersonalItems.Any() &&
													!a.Vehicles.Any())
										 .Include(a => a.AccountBalances)
										 .ToListAsync(cancellationToken) :
				await _dbContext.Accounts.Where(a => a.OwnerId == ownerId &&
													 a.Type == AccountType.Loan &&
													 a.LoanType != LoanType.Mortgage &&
													!a.PersonalItems.Any() &&
													!a.Vehicles.Any() &&
													 a.Name.Contains(search))
										 .Include(a => a.AccountBalances)
										 .ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<Account>> GetUserBudAccounts(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Accounts
				.Where(a => a.OwnerId == ownerId && a.Source == AccountSource.BudApi)
				.Include(a => a.AccountBalances)
				.Include(a => a.Transactions)
				.ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<string>> GetUserAccountsProviders(CustomerId ownerId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Accounts
				.Where(a => a.OwnerId == ownerId && a.Source == AccountSource.BudApi)
				.Select(a => a.Provider)
				.Distinct()
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<Account>> GetFilteredAccounts(CustomerId customerId, IEnumerable<AccountType> assetLiabilityTypes, IEnumerable<Guid> vaultEntries, bool archived, int? year = null, CancellationToken cancellationToken = default)
		{
			var result = _dbContext.Accounts
				.Include(a => a.AccountBalances)
				.Where(a => a.OwnerId == customerId && 
							!assetLiabilityTypes.Contains(a.Type) &&
							!vaultEntries.Contains(a.Id));

			if(!archived)
            {
				result = result.Where(a => a.IsArchived == archived);
            }

			if (year != null)
			{
				result = result.Where(a => a.AccountBalances.Any(x => x.EntryDate.Year == year));
			}

			return await result
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

        public async Task<bool> AccountExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId)
        {
			return await _dbContext.Accounts.AnyAsync(account => account.OwnerId == userId && account.Id != accountId 
			&& account.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}