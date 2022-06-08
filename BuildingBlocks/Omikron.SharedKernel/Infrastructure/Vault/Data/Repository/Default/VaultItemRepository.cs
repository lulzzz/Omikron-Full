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

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class VaultItemRepository : RepositoryBase<VaultItem>, IVaultItemRepository
    {
        private readonly VaultServiceDatabaseContext _dbContext;

        public VaultItemRepository(VaultServiceDatabaseContext repositoryContext) : base(repositoryContext)
        {
            _dbContext = repositoryContext;
        }

        public async Task<VaultItem> GetVaultItemByHostId(Guid hostId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.VaultItems.FirstOrDefaultAsync(x => x.HostId == hostId, cancellationToken);
        }

        public async Task<IEnumerable<VaultItem>> GetOwnerVaultItems(CustomerId userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.VaultItems.Where(vi => vi.OwnerId == userId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<VaultItem>> GetOwnerVaultItemsByAccountTypes(CustomerId userId, IEnumerable<AccountType> accountTypes, IEnumerable<VaultItemType> itemTypes, CancellationToken cancellationToken = default)
        {
            return await _dbContext.VaultItems.Where(vi => vi.OwnerId == userId && 
                                                    !accountTypes.Contains(vi.AccountType) &&
                                                    !itemTypes.Contains(vi.ItemType))
                                                    .ToListAsync(cancellationToken);
        }

        public async Task DeleteVaultItemsByUserId(CustomerId userId, CancellationToken cancellationToken = default)
        {
            var vaultItems = await GetOwnerVaultItems(userId, cancellationToken);

            if (vaultItems.Any())
            {
                _dbContext.VaultItems.RemoveRange(vaultItems);
            }
        }

        public async Task<bool> DeleteManualItem(Guid accountId, CancellationToken cancellationToken)
        {
            var vaultItem = await _dbContext.VaultItems.FirstOrDefaultAsync(x => x.HostId == accountId, cancellationToken);

            if (vaultItem == null)
            {
                return false;
            }

            _dbContext.VaultItems.Remove(vaultItem);

            return true;
        }

        public async Task<IEnumerable<VaultItem>> GetVaultItemsByHostIds(IEnumerable<Guid> hostIds, CancellationToken cancellationToken = default)
		{
            return await _dbContext.VaultItems
                .Where(v => hostIds.Contains(v.HostId))
                .ToListAsync(cancellationToken);
		}

        public async Task<IEnumerable<VaultItem>> GetUserAccountsByProvider(CustomerId ownerId, string provider, CancellationToken cancellationToken = default)
		{
            return await _dbContext.VaultItems
                .Where(v => v.ItemType == VaultItemType.Account &&
                            v.OwnerId == ownerId &&
                            v.AccountProvider == provider)
                .ToListAsync(cancellationToken);
		}

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}