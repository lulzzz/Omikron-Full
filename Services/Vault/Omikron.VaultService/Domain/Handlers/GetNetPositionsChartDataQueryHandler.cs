using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Extensions;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetNetPositionsChartDataQueryHandler : BaseHandlerLight<GetNetPositionsChartData.Query, ApiResult<IEnumerable<NetPositionsChartViewModel>>>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IPropertyRepository _propertyRepository;
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IPersonalItemRepository _personalItemRepository;
		private readonly IHttpIdentityService _httpIdentityService;
		private readonly IInvestmentRepository _investmentRepository;
		private readonly IAccountService _accountService;

		public GetNetPositionsChartDataQueryHandler(IAccountRepository accountRepository, IPropertyRepository propertyRepository,
			IVehicleRepository vehicleRepository, IPersonalItemRepository personalItemRepository, IHttpIdentityService httpIdentityService,
			IInvestmentRepository investmentRepository, IAccountService accountService)
		{
			_accountRepository = accountRepository;
			_propertyRepository = propertyRepository;
			_vehicleRepository = vehicleRepository;
			_personalItemRepository = personalItemRepository;
			_httpIdentityService = httpIdentityService;
			_investmentRepository = investmentRepository;
			_accountService = accountService;
		}

		public override async Task<ApiResult<IEnumerable<NetPositionsChartViewModel>>> Handle(GetNetPositionsChartData.Query request, CancellationToken cancellationToken)
		{
			var result = request.MonthMode ? InitiateResult(1, SharedKernel.Constants.NumberOfMonthsInYear)
										   : await FactoryYearModeResult(cancellationToken);

			var accountTypes = AccountType.EnumerateTypesAsDisplayNames();
			var assetLiabilityTypes = request.AssetLiabilityTypes?.Where(x => accountTypes.Contains(x)).Select(x => AccountType.Parse(x)) ?? new List<AccountType>();
			var vaultEntries = request.VaultEntries ?? new List<Guid>();

			var accounts = await _accountRepository.GetFilteredAccounts(CustomerId.Parse(request.UserId), assetLiabilityTypes, vaultEntries, request.ArchivedAccounts, request.Year, cancellationToken);
			CalculateAccountBalances(result, accounts, request);

			if (DoCalculation(request, VaultItemType.Property))
			{
				var properties = await _propertyRepository.GetFilteredProperties(CustomerId.Parse(request.UserId), vaultEntries, request.ArchivedAccounts, request.Year, cancellationToken);
				CalculatePropertyValues(result, properties, request);
			}

			if (DoCalculation(request, VaultItemType.Vehicle))
			{
				var vehicles = await _vehicleRepository.GetFilteredVehicles(CustomerId.Parse(request.UserId), vaultEntries, request.ArchivedAccounts, request.Year, cancellationToken);
				CalculateVehicleValues(result, vehicles, request);
			}

			if (DoCalculation(request, VaultItemType.PersonalItem))
			{
				var personalItems = await _personalItemRepository.GetFilteredPersonalItems(CustomerId.Parse(request.UserId), vaultEntries, request.ArchivedAccounts, request.Year, cancellationToken);
				CalculatePersonalItemValues(result, personalItems, request);
			}

			if (DoCalculation(request, VaultItemType.Investment))
			{
				var investments = await _investmentRepository.GetFilteredInvestments(CustomerId.Parse(request.UserId), vaultEntries, request.ArchivedAccounts, request.Year, cancellationToken);
				CalculateInvestmentValues(result, investments, request);
			}

			return ApiResult<IEnumerable<NetPositionsChartViewModel>>.Success().WithData(result.Select(x => new NetPositionsChartViewModel(x.Key, x.Value)));
		}

		private async Task<Dictionary<int, GetSummaryViewModel>> FactoryYearModeResult(CancellationToken cancellationToken)
		{
			var userRegistrationYear = await _httpIdentityService.GetUserRegistrationDate<UserRegistrationDateResponse>(cancellationToken);
			var minYear = userRegistrationYear.Records.Year;
			var maxYear = Clock.GetTime().Year;

			var result = InitiateResult(minYear, maxYear);

			return result;
		}

		private static Dictionary<int, GetSummaryViewModel> InitiateResult(int minYear, int maxYear)
		{
			var result = new Dictionary<int, GetSummaryViewModel>();

			for (int i = minYear; i <= maxYear; i++)
			{
				result.Add(i, new());
			}

			return result;
		}

		private static bool DoCalculation<T>(GetNetPositionsChartData.Query request, T itemType) where T : VaultItemType
		{
			return request.AssetLiabilityTypes == null ||
				   (request.AssetLiabilityTypes != null && !request.AssetLiabilityTypes.Contains(itemType.ToString().ToAccountGroupDisplayName()));
		}

		private static void CalculateInvestmentValues(Dictionary<int, GetSummaryViewModel> result, IEnumerable<Investment> investments, GetNetPositionsChartData.Query request)
		{
			foreach (var investment in investments)
			{
				var valueGroups = request.MonthMode ? investment.InvestmentValues.Where(i => i.EntryDate.Year == request.Year).GroupBy(p => p.EntryDate.Month) :
													  investment.InvestmentValues.GroupBy(i => i.EntryDate.Year);
				if (valueGroups.IsNullOrEmpty())
				{
					continue;
				}

				valueGroups = valueGroups.OrderBy(x => x.Key);
				AppendValueToResult(result, request, valueGroups);
			}
		}

		private static void CalculatePersonalItemValues(Dictionary<int, GetSummaryViewModel> result, IEnumerable<PersonalItem> personalItems, GetNetPositionsChartData.Query request)
		{
			foreach (var personalItem in personalItems)
			{
				var valueGroups = request.MonthMode ? personalItem.PersonalItemValues.Where(p => p.EntryDate.Year == request.Year).GroupBy(p => p.EntryDate.Month) :
													  personalItem.PersonalItemValues.GroupBy(p => p.EntryDate.Year);
				if (valueGroups.IsNullOrEmpty())
				{
					continue;
				}

				valueGroups = valueGroups.OrderBy(x => x.Key);
				AppendValueToResult(result, request, valueGroups);
			}
		}

		private static void CalculateVehicleValues(Dictionary<int, GetSummaryViewModel> result, IEnumerable<Vehicle> vehicles, GetNetPositionsChartData.Query request)
		{
			foreach (var vehicle in vehicles)
			{
				var valueGroups = request.MonthMode ? vehicle.VehicleValues.Where(v => v.EntryDate.Year == request.Year).GroupBy(p => p.EntryDate.Month) :
													  vehicle.VehicleValues.GroupBy(v => v.EntryDate.Year);

				if (valueGroups.IsNullOrEmpty())
				{
					continue;
				}

				valueGroups = valueGroups.OrderBy(x => x.Key);
				AppendValueToResult(result, request, valueGroups);
			}
		}

		private static void CalculatePropertyValues(Dictionary<int, GetSummaryViewModel> result, IEnumerable<Property> properties, GetNetPositionsChartData.Query request)
		{
			foreach (var property in properties)
			{
				var valueGroups = request.MonthMode ? property.PropertyValues.Where(p => p.EntryDate.Year == request.Year).GroupBy(p => p.EntryDate.Month) :
													  property.PropertyValues.GroupBy(p => p.EntryDate.Year);

				if (valueGroups.IsNullOrEmpty())
				{
					continue;
				}

				valueGroups = valueGroups.OrderBy(x => x.Key);
				AppendValueToResult(result, request, valueGroups);
			}
		}

		private void CalculateAccountBalances(Dictionary<int, GetSummaryViewModel> result, IEnumerable<Account> accounts, GetNetPositionsChartData.Query request)
		{
			foreach (var account in accounts)
			{
				var balanceGroups = request.MonthMode ? account.AccountBalances.Where(b => b.EntryDate.Year == request.Year).GroupBy(b => b.EntryDate.Month) :
														account.AccountBalances.GroupBy(b => b.EntryDate.Year);

				if (balanceGroups.IsNullOrEmpty())
				{
					continue;
				}

				balanceGroups = balanceGroups.OrderBy(x => x.Key);
				var firstBalanceGroup = balanceGroups.FirstOrDefault();
				var lastKey = GetLastKey(request);

				var previousBalanceAmount = 0m;
				var previousSubType = AccountSubType.Asset;

				for (var i = firstBalanceGroup.Key; i <= lastKey; i++)
				{
					if (!result.TryGetValue(i, out var _))
					{
						continue;
					}

					var balances = balanceGroups.FirstOrDefault(x => x.Key == i);
					if (!balances.IsNullOrEmpty())
					{
						var lastBalance = _accountService.GetLastBalance(balances, account.Type);
						var lastBalanceCreditDebitIndicator = lastBalance != null ? lastBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;
						var lastBalanceAmount = lastBalance != null ? lastBalance.Amount : 0;

						var subType = AccountSubType.Parse(account.Type, lastBalanceCreditDebitIndicator == CreditDebitIndicator.Credit ? lastBalanceAmount : lastBalanceAmount * (-1));
						AppendValueToResult(result, i, lastBalanceAmount, subType);

						previousBalanceAmount = lastBalanceAmount;
						previousSubType = subType;
					}
					else
					{
						AppendValueToResult(result, i, previousBalanceAmount, previousSubType);
					}
				}
			}
		}

		private static void AppendValueToResult(Dictionary<int, GetSummaryViewModel> result, int i, decimal lastBalanceAmount, AccountSubType subType)
		{
			if (subType == AccountSubType.Asset)
			{
				result[i].Assets += lastBalanceAmount;
			}
			else
			{
				result[i].Liabilities += lastBalanceAmount;
			}
		}

		private static void AppendValueToResult<T>(Dictionary<int, GetSummaryViewModel> result, GetNetPositionsChartData.Query request, IEnumerable<IGrouping<int, T>> valueGroups) where T : VaultItemValue<Guid>
		{
			var firstValueGroup = valueGroups.FirstOrDefault();
			var lastKey = GetLastKey(request);

			var previousValue = 0m;

			for (var i = firstValueGroup.Key; i <= lastKey; i++)
			{
				if (!result.TryGetValue(i, out var _))
				{
					continue;
				}

				var values = valueGroups.FirstOrDefault(x => x.Key == i);
				if (!values.IsNullOrEmpty())
				{
					var lastValue = values.OrderByDescending(b => b.EntryDate).FirstOrDefault()?.Amount ?? 0;
					result[i].Assets += lastValue;

					previousValue = lastValue;
				}
				else
				{
					result[i].Assets += previousValue;
				}
			}
		}

		private static int GetLastKey(GetNetPositionsChartData.Query request)
		{
			var currentTime = Clock.GetTime();
			return request.MonthMode
				? GetMonthIndex(request, currentTime)
				: currentTime.Year;
		}

		private static int GetMonthIndex(GetNetPositionsChartData.Query request, DateTime currentTime)
		{
			return request.Year.HasValue && request.Year.Value == currentTime.Year ? currentTime.Month : SharedKernel.Constants.NumberOfMonthsInYear;
		}
	}
}
