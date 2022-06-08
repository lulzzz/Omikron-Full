using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetMerchantsQueryHandler : BaseHandlerLight<GetMerchants.Query, ApiResult<MerchantContainerViewModel>>
	{
		private readonly IMerchantRepository _merchantRepository;

		public GetMerchantsQueryHandler(IMerchantRepository merchantRepository)
		{
			_merchantRepository = merchantRepository;
		}

		public override async Task<ApiResult<MerchantContainerViewModel>> Handle(GetMerchants.Query request, CancellationToken cancellationToken)
		{
			var dateFilter = CreateDateFilter(request);
			var accountTypes = AccountType.EnumerateTypesAsDisplayNames();
			var assetLiabilityTypes = request.AssetLiabilityTypes?.Where(x => accountTypes.Contains(x)).Select(x => AccountType.Parse(x)) ?? new List<AccountType>();
			var vaultEntries = request.VaultEntries ?? new List<Guid>();
			var categories = request.Categories ?? new List<string>();

			var filteredMerchants = await _merchantRepository.GetFilteredMerchants(CustomerId.Parse(request.UserId), 
																			       dateFilter,
																				   request.Page,
																				   request.PageSize,
																				   assetLiabilityTypes,
																				   vaultEntries,
																				   categories, 
																				   cancellationToken);

			var result = await _merchantRepository.GetMerchantData(CustomerId.Parse(request.UserId), dateFilter, cancellationToken);
			result.Merchants = filteredMerchants;

			return ApiResult<MerchantContainerViewModel>.Success().WithData(result);
		}

		private static DateRange CreateDateFilter(GetMerchants.Query request)
		{
			return request.MonthIndex.HasValue ? new DateRange(new DateTime(request.Year, request.MonthIndex.Value, 1),
															   new DateTime(request.Year, request.MonthIndex.Value, DateTime.DaysInMonth(request.Year, request.MonthIndex.Value)))
											   : new DateRange(new DateTime(request.Year, 1, 1),
															   new DateTime(request.Year, SharedKernel.Constants.NumberOfMonthsInYear, DateTime.DaysInMonth(request.Year, SharedKernel.Constants.NumberOfMonthsInYear)));
		}
	}
}
