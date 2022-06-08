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
    public interface IPropertyRepository : IRepositoryBase<Property>
    {
        Task<IEnumerable<Property>> GetPropertiesByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default);
        Task DeletePropertiesByOwnerId(CustomerId userId, CancellationToken cancellationToken = default);
        Task<ManualAccountDetailsViewModel> FindPropertyWithTransactionHistory(Guid accountId, CancellationToken cancellationToken);
        Task<bool> RemoveProperty(Guid accountId, CancellationToken cancellationToken);
        Task<PropertyViewModel> GetPropertyDetails(Guid accountId, CancellationToken cancellationToken);
        Task<Property> GetProperty(Guid accountId, CancellationToken cancellationToken);
        string GetPropertyIdByMortgageId(Guid mortgageId);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
        Task<bool> PropertyExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId = null);
        Task<IEnumerable<Property>> GetFilteredProperties(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken);
        Task<IEnumerable<Property>> GetPropertiesToRevalue(CancellationToken cancellationToken);
    }
}
