using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        private readonly VaultServiceDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public PropertyRepository(VaultServiceDatabaseContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task DeletePropertiesByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
        {
            var properties = await GetPropertiesByOwnerId(ownerId, cancellationToken);

            if (properties.Any())
            {
                _dbContext.Properties.RemoveRange(properties);
            }
        }

        public async Task<ManualAccountDetailsViewModel> FindPropertyWithTransactionHistory(Guid accountId, CancellationToken cancellationToken)
        {
            var property = await _dbContext.Properties
                .Include(x => x.PropertyValues)
                .Include(x => x.Mortgage).ThenInclude(x => x.AccountBalances)
                .Where(x => x.Id == accountId)
                .ProjectTo<ManualAccountDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return property;
        }

        public async Task<bool> RemoveProperty(Guid accountId, CancellationToken cancellationToken)
        {
            var property = await _dbContext.Properties
            .Include(p => p.Mortgage)
            .FirstOrDefaultAsync(p => p.Id == accountId, cancellationToken);

            if (property == null)
            {
                return false;
            }

            if (property.Mortgage.IsNotNull())
            {
                property.Mortgage.LoanType = null;
            }

            _dbContext.Properties.Remove(property);

            return true;
        }

        public async Task<PropertyViewModel> GetPropertyDetails(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Properties
                .Where(x => x.Id == accountId)
                .ProjectTo<PropertyViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Property> GetProperty(Guid accountId, CancellationToken cancellationToken)
        {
            return await _dbContext.Properties.Include(x => x.Mortgage).FirstOrDefaultAsync(x => x.Id == accountId, cancellationToken);
        }

        public string GetPropertyIdByMortgageId(Guid mortgageId)
        {
            return _dbContext.Properties
                .Where(v => v.MortgageId == mortgageId)
                .Select(v => v.Id.ToString()).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Properties
                .Where(p => p.OwnerId == ownerId)
                .Include(p => p.PropertyValues)
                .ToListAsync(cancellationToken);
        }

		public async Task<IEnumerable<Property>> GetFilteredProperties(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken)
		{
            var result = _dbContext.Properties
                .Include(a => a.PropertyValues)
                .Where(a => a.OwnerId == customerId &&
                            !vaultEntries.Contains(a.Id));

            if (!archived)
            {
                result = result.Where(a => a.IsArchived == archived);
            }

            if (year != null)
            {
                result = result.Where(p => p.PropertyValues.Any(x => x.EntryDate.Year == year));
            }

            return await result
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> PropertyExists(CustomerId userId, string name, CancellationToken cancellationToken, Guid? accountId)
        {
            return await _dbContext.Properties.AnyAsync(account => account.OwnerId == userId && account.Id != accountId
             && account.Name.ToLower() == name.ToLower(), cancellationToken);
        }

		public async Task<IEnumerable<Property>> GetPropertiesToRevalue(CancellationToken cancellationToken)
		{
            return await _dbContext.Properties
                .Include(v => v.PropertyValues)
                .Where(v => v.AutomaticallyReValueProperty)
                .ToListAsync(cancellationToken);
        }
	}
}
