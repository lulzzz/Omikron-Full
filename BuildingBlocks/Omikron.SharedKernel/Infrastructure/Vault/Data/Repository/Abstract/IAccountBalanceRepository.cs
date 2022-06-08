using System;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
    public interface IAccountBalanceRepository : IRepositoryBase<AccountBalance>
    {
        Task<decimal> GetLatestManualAccountBalance(Guid accountId, CancellationToken cancellationToken);
    }
}