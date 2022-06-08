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
    public interface IVehicleRepository : IRepositoryBase<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetVehiclesByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task DeleteVehiclesByUserId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task<ManualAccountDetailsViewModel> FindVehicleWithTransactionHistoryAndFinance(Guid accountId, CancellationToken cancellationToken);
        Task<bool> RemoveVehicle(Guid accountId, CancellationToken cancellationToken);
        Task<VehicleViewModel> GetVehicleDetails(Guid accountId, CancellationToken cancellationToken);
        Task<Vehicle> GetVehicle(Guid accountId, CancellationToken cancellationToken);
        string GetVehicleIdByFinanceAgreementId (Guid financeAgreementId);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
        Task<bool> VehicleExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId = null);
        Task<IEnumerable<Vehicle>> GetFilteredVehicles(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken);
        Task<IEnumerable<Vehicle>> GetVehiclesToRevalue(CancellationToken cancellationToken);
    }
}