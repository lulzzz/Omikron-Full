using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.VaultService.Domain.Queries.ManualAccountDetails
{
    public class GetAccountDetails
    {
        public class Query : ManualAccountDetailsBaseQuery
        {
            public AccountType AccountType { get; set; }
        }
    }
}