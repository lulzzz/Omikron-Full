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
    public interface IInvestmentRepository : IRepositoryBase<Investment>
    {
        Task<IEnumerable<Investment>> GetInvestmentsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task DeleteInvestmentsByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<ManualAccountDetailsViewModel> GetInvestmentWithTransactionHistory(Guid accountId, CancellationToken cancellationToken);
        Task<bool> DeleteInvestmentById(Guid accountId, CancellationToken cancellationToken);
        Task<InvestmentViewModel> GetInvestmentDetails(Guid accountId, CancellationToken cancellationToken);
        Task<Investment> GetInvestment(Guid accountId, CancellationToken cancellationToken);
        Task<bool> SaveChangesAsync();
        Task<bool> InvestmentExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId = null);
        Task<IEnumerable<Investment>> GetFilteredInvestments(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken);
	}
}