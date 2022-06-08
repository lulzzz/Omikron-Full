using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using System;
using System.Collections.Generic;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetVaultEntryList
    {
        public class Query : BaseCommand.Action<ApiResult<IEnumerable<VaultEntryListViewModel>>>
        {
            public Guid UserId { get; set; }
            public IEnumerable<string> AssetLiabilityTypes { get; set; }

            public Query(Guid userId, IEnumerable<string> assetLiabilityTypes)
            {
                UserId = userId;
                AssetLiabilityTypes = assetLiabilityTypes;
            }

            public Query()
            {

            }
        }
    }
}