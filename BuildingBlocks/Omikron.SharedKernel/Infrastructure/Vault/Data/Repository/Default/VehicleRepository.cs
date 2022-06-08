using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        private readonly VaultServiceDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public VehicleRepository(VaultServiceDatabaseContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task DeleteVehiclesByUserId(CustomerId ownerId, CancellationToken cancellationToken = default)
        {
            var vehicles = await GetVehiclesByOwnerId(ownerId, cancellationToken);

            if (vehicles.Any())
            {
                _dbContext.RemoveRange(vehicles);
            }
        }

        public async Task<Vehicle> GetVehicle(Guid accountId, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles
                .Include(x => x.FinancialAgreement)
                .Where(x => x.Id == accountId)
                .FirstOrDefaultAsync(cancellationToken);

            return vehicle;
        }

        public async Task<ManualAccountDetailsViewModel> FindVehicleWithTransactionHistoryAndFinance(Guid accountId, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles
                .Include(x => x.FinancialAgreement).ThenInclude(x =>x.AccountBalances)
                .Include(x => x.VehicleValues)
                .Where(x => x.Id == accountId)
                .ProjectTo<ManualAccountDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return vehicle;
        }

        public async Task<bool> RemoveVehicle(Guid accountId, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles
            .Include(p => p.FinancialAgreement)
            .FirstOrDefaultAsync(p => p.Id == accountId, cancellationToken);

            if (vehicle == null)
            {
                return false;
            }

            if (vehicle.FinancialAgreement.IsNotNull())
            {
                vehicle.FinancialAgreement.LoanType = null;
            }

            _dbContext.Vehicles.Remove(vehicle);

            return true;
        }

        public async Task<VehicleViewModel> GetVehicleDetails(Guid accountId, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles
                .Where(x => x.Id == accountId)
                .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByOwnerId(CustomerId ownerId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Vehicles
                .Where(v => v.OwnerId == ownerId)
                .Include(v => v.VehicleValues)
                .ToListAsync(cancellationToken);
        }

        public string GetVehicleIdByFinanceAgreementId(Guid financeAgreementId)
        {
            return _dbContext.Vehicles
                .Where(v => v.FinancialAgreementId == financeAgreementId)
                .Select(v => v.Id.ToString()).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

		public async Task<IEnumerable<Vehicle>> GetFilteredVehicles(CustomerId customerId, IEnumerable<Guid> vaultEntries, bool archived, int? year, CancellationToken cancellationToken)
		{
            var result = _dbContext.Vehicles
                .Include(a => a.VehicleValues)
                .Where(a => a.OwnerId == customerId &&
                            !vaultEntries.Contains(a.Id));

            if (!archived)
            {
                result = result.Where(a => a.IsArchived == archived);
            }

            if (year != null)
            {
                result = result.Where(p => p.VehicleValues.Any(x => x.EntryDate.Year == year));
            }

            return await result
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

		public async Task<IEnumerable<Vehicle>> GetVehiclesToRevalue(CancellationToken cancellationToken)
		{
            return await _dbContext.Vehicles
                .Include(v => v.VehicleValues)
                .Where(v => v.AutomaticallyReValueVehicle)
                .ToListAsync(cancellationToken);
		}

        public async Task<bool> VehicleExists(CustomerId userId,  string name, CancellationToken cancellationToken, Guid? accountId)
        {
            return await _dbContext.Vehicles.AnyAsync(account => account.OwnerId == userId && account.Id != accountId
            && account.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
