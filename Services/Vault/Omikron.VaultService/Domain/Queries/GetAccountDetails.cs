using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetAccountDetails
    {
        public class Query : BaseCommand.Action<ApiResult<AccountViewModel>>
        {
            public Guid AccountId { get; set; }

            public Query(Guid accountId)
            {
                AccountId = accountId;
            }
        }
    }
}
