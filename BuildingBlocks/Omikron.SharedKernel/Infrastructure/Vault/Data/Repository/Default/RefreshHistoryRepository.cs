using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class RefreshHistoryRepository : IRefreshHistoryRepository
    {
        private readonly VaultServiceDatabaseContext _dbContext;

        public RefreshHistoryRepository(VaultServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshHistory> AddRefresh(Guid userId, CancellationToken cancellationToken = default)
        {
            var refreshHistory = new RefreshHistory(userId);
            await _dbContext.RefreshHistories.AddAsync(refreshHistory, cancellationToken);
            return refreshHistory;
        }

        public async Task<RefreshHistory> GetLastRefresh(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshHistories.Where(rh => rh.UserId == userId).OrderBy(rh => rh.CreatedAt).LastOrDefaultAsync(cancellationToken);
        }

        public async Task DeleteRefreshHistoryByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            var refreshHistories = await GetRefreshHistoriesByUserId(userId, cancellationToken);

            if (refreshHistories.Any())
            {
                _dbContext.RefreshHistories.RemoveRange(refreshHistories);
            }
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<RefreshHistory>> GetRefreshHistoriesByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshHistories.Where(rh => rh.UserId == userId).ToListAsync(cancellationToken);
        }
    }
}
