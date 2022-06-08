using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
    public interface IPersonalItemRepository : IRepositoryBase<PersonalItem>
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task DeletePersonalItemsByUserId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PersonalItem>> GetPersonalItemsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<PersonalItemViewModel> GetPersonalItemDetails(Guid accountId, CancellationToken cancellationToken);
        Task<PersonalItem> FindPersonalItemWithTransactionHistoryAndFinance(Guid accountId, CancellationToken cancellationToken);
        Task<ManualAccountDetailsViewModel> GetPersonalItemWithTransactions(Guid accountId, CancellationToken cancellationToken);
        string GetPersonalItemIdByFinanceAgreementId(Guid financeAgreementId);
        Task<PersonalItem> GetPersonalItem(Guid accountId, CancellationToken cancellationToken);
        Task<bool> RemovePersonalItemById(Guid accountId, CancellationToken cancellationToken);
        Task<bool> PersonalItemExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId = null);
        Task<IEnumerable<PersonalItem>> GetFilteredPersonalItems(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken);
	}
}
