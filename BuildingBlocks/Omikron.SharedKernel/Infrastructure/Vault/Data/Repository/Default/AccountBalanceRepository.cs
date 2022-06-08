using System;
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
    public class AccountBalanceRepository : RepositoryBase<AccountBalance>, IAccountBalanceRepository
    {
        private readonly VaultServiceDatabaseContext _dbContext;

        public AccountBalanceRepository(VaultServiceDatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> GetLatestManualAccountBalance(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.AccountBalances
                .Where(x => x.AccountId == accountId && x.Account.Source == AccountSource.Manual)
                .OrderByDescending(x => x.EntryDate)
                .Select(x => x.Amount)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}