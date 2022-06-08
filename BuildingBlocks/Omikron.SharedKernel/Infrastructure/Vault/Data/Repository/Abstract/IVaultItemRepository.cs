using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
    public interface IVaultItemRepository : IRepositoryBase<VaultItem>
    {
        Task<IEnumerable<VaultItem>> GetUserAccountsByProvider(CustomerId ownerId, string provider, CancellationToken cancellationToken = default);
        Task<IEnumerable<VaultItem>> GetVaultItemsByHostIds(IEnumerable<Guid> hostIds, CancellationToken cancellationToken = default);
        Task<VaultItem> GetVaultItemByHostId(Guid hostId, CancellationToken cancellationToken = default);
        Task<IEnumerable<VaultItem>> GetOwnerVaultItems(CustomerId userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<VaultItem>> GetOwnerVaultItemsByAccountTypes(CustomerId userId, IEnumerable<AccountType> accountTypes, IEnumerable<VaultItemType> itemTypes, CancellationToken cancellationToken = default);
        Task DeleteVaultItemsByUserId(CustomerId userId, CancellationToken cancellationToken = default);
        Task<bool> DeleteManualItem(Guid accountId, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}