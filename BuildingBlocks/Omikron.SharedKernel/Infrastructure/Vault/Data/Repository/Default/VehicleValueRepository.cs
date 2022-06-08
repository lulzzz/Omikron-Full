using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class VehicleValueRepository : RepositoryBase<VehicleValue>, IVehicleValueRepository
	{
		public VehicleValueRepository(VaultServiceDatabaseContext repositoryContext) : base(repositoryContext)
		{
		}
	}
}
