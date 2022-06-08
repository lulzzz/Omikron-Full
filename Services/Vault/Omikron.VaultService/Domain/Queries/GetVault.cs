using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;

namespace Omikron.VaultService.Domain.Queries
{
	public class GetVault
    {
        public class Query : BaseCommand.Action<ApiResult<VaultViewModel>>
        {
            public Guid UserId { get; set; }

            public Query(Guid userId)
            {
                UserId = userId;
            }
        }
    }
}
