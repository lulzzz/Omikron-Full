using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.VaultService.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<IEnumerable<Transaction>> SearchTransactionsByAccountId(Guid accountId, string searchTerm = "", CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaction>> GetAllUserTransactions(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionsPerCategoryCountContainer>> GetNumberOfTransactionsPerCategoryAsync(CustomerId ownerId, DateRange dateFilter, CancellationToken cancellationToken = default);
		Task<Transaction> GetFirstTransactionByDate(CustomerId userId, CancellationToken cancellationToken);
		Task<IEnumerable<CategoryData>> GetFilteredCategories(CustomerId customerId, DateRange dateFilter, IEnumerable<AccountType> assetLiabilityTypes, IEnumerable<Guid> vaultEntries, IEnumerable<string> categories, CancellationToken cancellationToken);
    }
}
