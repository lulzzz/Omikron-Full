using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
	public class PersonalItemValueRepository : RepositoryBase<PersonalItemValue>, IPersonalItemValueRepository
	{
		public PersonalItemValueRepository(VaultServiceDatabaseContext repositoryContext) : base(repositoryContext)
		{
		}
	}
}
