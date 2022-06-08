using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
    public interface IRefreshHistoryRepository
    {
        Task<RefreshHistory> AddRefresh(Guid userId, CancellationToken cancellationToken = default);
        Task<RefreshHistory> GetLastRefresh(Guid userId, CancellationToken cancellationToken = default);
        Task DeleteRefreshHistoryByUserId(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RefreshHistory>> GetRefreshHistoriesByUserId(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}