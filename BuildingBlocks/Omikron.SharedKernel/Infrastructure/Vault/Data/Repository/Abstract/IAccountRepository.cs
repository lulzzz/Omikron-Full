using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> GetAccountsWithBalanceByOwnerId(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetUserAccountsByProvider(CustomerId ownerId, string provider, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetUserAccountsProviders(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetAccountsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetUserBudAccounts(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> SearchLoans(CustomerId ownerId, string search, CancellationToken cancellationToken = default);
        Task<Account> GetUserAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetFilteredAccounts(CustomerId customerId, IEnumerable<AccountType> assetLiabilityTypes, IEnumerable<Guid> vaultEntries, bool archived, int? year = null, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task DeleteAccountsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAccountByExternalId(Guid externalId, CancellationToken cancellationToken);
        Task<MortgageManualAccountViewModel> GetPropertyMortgageAccount(Guid financeId, Guid accountId, CancellationToken cancellationToken);
        Task<VehicleFinanceViewModel> GetVehicleFinanceAccount(Guid financeId, Guid accountId, CancellationToken cancellationToken);
        Task<bool> DeleteMortgage(Guid accountId, CancellationToken cancellationToken);
        Task<bool> DeleteVehicleFinance(Guid accountId, CancellationToken cancellationToken);
        Task<PersonalItemFinanceViewModel> GetPersonalItemFinanceAccount(Guid financeId, Guid accountId, CancellationToken cancellationToken);
        Task<bool> DeletePersonalItemFinance(Guid accountId, CancellationToken cancellationToken);

        Task<ManualAccountDetailsViewModel> GetManualAccountDetails(Guid accountId, CancellationToken cancellationToken);
        Task<Account> GetAccount(Guid accountId, CancellationToken cancellationToken);
        Task<ManualAccountViewModel> GetManualAccount(Guid accountId, CancellationToken cancellationToken);
        Task<bool> AccountExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId = null);
    }
}