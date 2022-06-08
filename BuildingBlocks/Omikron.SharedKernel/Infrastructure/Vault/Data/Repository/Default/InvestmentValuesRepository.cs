using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Default
{
    public class InvestmentValuesRepository : RepositoryBase<InvestmentValue>, IInvestmentValuesRepository
    {
        public InvestmentValuesRepository(VaultServiceDatabaseContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}