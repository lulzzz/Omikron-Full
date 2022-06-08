using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Extensions;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetVaultEntryListQueryHandler : BaseHandlerLight<GetVaultEntryList.Query, ApiResult<IEnumerable<VaultEntryListViewModel>>>
	{
		private readonly IVaultItemRepository _vaultItemRepository;

		public GetVaultEntryListQueryHandler(IVaultItemRepository vaultItemRepository)
		{
			_vaultItemRepository = vaultItemRepository;
		}

		public override async Task<ApiResult<IEnumerable<VaultEntryListViewModel>>> Handle(GetVaultEntryList.Query request, CancellationToken cancellationToken)
		{
			var accountTypes = AccountType.EnumerateTypesAsDisplayNames();
			var itemTypes = VaultItemType.EnumerateTypesAsDisplayNames();
            var filteredAccountTypes = request.AssetLiabilityTypes?.Where(x => accountTypes.Contains(x)).Select(x => AccountType.Parse(x)) ?? new List<AccountType>();
            var filteredItemTypes = request.AssetLiabilityTypes?.Where(x => itemTypes.Contains(x)).Select(x => VaultItemType.Parse(x)) ?? new List<VaultItemType>();

            var vaultItems = await _vaultItemRepository.GetOwnerVaultItemsByAccountTypes(CustomerId.Parse(request.UserId), filteredAccountTypes, filteredItemTypes, cancellationToken);

            var result = vaultItems.Where(v => v.ItemType == VaultItemType.Account)
                                   .GroupBy(v => v.AccountType)
                                   .Select(g => new VaultEntryListViewModel()
                                   {
                                       GroupName = g.Key.ToString().ToAccountGroupDisplayName(),
                                       VaultEntries = g.Select(x => new VaultEntryViewModel()
                                       {
                                           VaultEntryId = x.HostId.ToString(),
                                           VaultEntryName = x.Name.IsNotNullOrEmpty() ? x.Name : $"{x.AccountProvider} {x.AccountIdentificationNumber?.ToString()}"
                                       })
                                   })
                                   .Concat(
                        vaultItems.Where(v => v.ItemType != VaultItemType.Account)
                                  .GroupBy(v => v.ItemType)
                                  .Select(g => new VaultEntryListViewModel()
                                  {
                                      GroupName = g.Key.ToString().ToAccountGroupDisplayName(),
                                      VaultEntries = g.Select(x => new VaultEntryViewModel()
                                      {
                                          VaultEntryId = x.HostId.ToString(),
                                          VaultEntryName = x.Name
                                      })
                                  }));


            return ApiResult<IEnumerable<VaultEntryListViewModel>>.Success().WithData(result);
        }
	}
}
