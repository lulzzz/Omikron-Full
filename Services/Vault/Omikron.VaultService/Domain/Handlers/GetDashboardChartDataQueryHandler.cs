using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetDashboardChartDataQueryHandler : BaseHandlerLight<GetDashboardChartData.Query, ApiResult<IEnumerable<DashboardChartViewModel>>>
	{
		private readonly IPersonalItemRepository _personalItemRepository;
		private readonly IInvestmentRepository _investmentRepository;
		private readonly IAccountService _accountService;
		private readonly IPropertyRepository _propertyRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IHttpIdentityService _httpIdentityService;

		public GetDashboardChartDataQueryHandler(IAccountRepository accountRepository, IPropertyRepository propertyRepository,
			IVehicleRepository vehicleRepository, IPersonalItemRepository personalItemRepository, IHttpIdentityService httpIdentityService,
			IInvestmentRepository investmentRepository, IAccountService accountService)
		{
			_personalItemRepository = personalItemRepository;
			_investmentRepository = investmentRepository;
			_accountService = accountService;
			_propertyRepository = propertyRepository;
			_accountRepository = accountRepository;
			_vehicleRepository = vehicleRepository;
			_httpIdentityService = httpIdentityService;
		}

		public override async Task<ApiResult<IEnumerable<DashboardChartViewModel>>> Handle(GetDashboardChartData.Query request, CancellationToken cancellationToken)
		{
			var userRegistration = await _httpIdentityService.GetUserRegistrationDate<UserRegistrationDateResponse>(cancellationToken);
			var registrationDate = new DateTime(userRegistration.Records.Year, userRegistration.Records.Month, userRegistration.Records.Day);
			var result = request.AllTimeFilter ? FactoryYearModeResult(registrationDate) :
												   InitiateResult(Clock.GetTime().AddMonths(-request.NumberOfMonths ?? 0), Clock.GetTime());

			var accounts = await _accountRepository.GetAccountsWithBalanceByOwnerId(request.UserId, cancellationToken);
			var properties = await _propertyRepository.GetPropertiesByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);
			var vehicles = await _vehicleRepository.GetVehiclesByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);
			var personalItems = await _personalItemRepository.GetPersonalItemsByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);
			var investments = await _investmentRepository.GetInvestmentsByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);

			CalculateVehicleValues(result, vehicles, request);
			CalculatePropertyValues(result, properties, request);
			CalculateAccountBalances(result, accounts, request);
			CalculateInvestmentValues(result, investments, request);
			CalculatePersonalItemValues(result, personalItems, request);

			var calculatedResult = result.Select(x => new DashboardChartViewModel(x.Key, x.Value)).ToList();

			int counter = 0;
			foreach (var group in calculatedResult)
			{
				if (group != calculatedResult.First())
				{
					group.Data = calculatedResult[counter++].Data + group.Data;
				}
			}

			return ApiResult<IEnumerable<DashboardChartViewModel>>.Success().WithData(calculatedResult.Where(x => x.Data.Net != 0 || x.DateIndex.Equals(registrationDate.ToString("dd. MM. yyyy.")) || x.DateIndex.Equals(registrationDate.ToString("MM. yyyy."))));
		}

        private static Dictionary<string, GetSummaryViewModel> FactoryYearModeResult(DateTime firstMonth)
        {
            var nowDate = Clock.GetTime();
            var lastMonth = new DateTime(nowDate.Year, nowDate.Month + 1, 1);

            return InitiateAllTimeResult(firstMonth, lastMonth);
        }

        private static Dictionary<string, GetSummaryViewModel> InitiateResult(DateTime firstDay, DateTime lastDay)
		{
			var result = new Dictionary<string, GetSummaryViewModel>();

			for (DateTime date = firstDay; date < lastDay; date = date.AddDays(1.0))
			{
				result.Add(date.ToString("dd. MM. yyyy."), new());
			}

			return result;
		}

		private static Dictionary<string, GetSummaryViewModel> InitiateAllTimeResult(DateTime firstMonth, DateTime lastMonth)
		{
			var result = new Dictionary<string, GetSummaryViewModel>();
			
			for (DateTime date = firstMonth; date < lastMonth; date = date.AddMonths(1))
			{
				result.Add(date.ToString("MM. yyyy."), new());
			}

			return result;
		}

		private static void CalculateVehicleValues(Dictionary<string, GetSummaryViewModel> result, IEnumerable<Vehicle> vehicles, GetDashboardChartData.Query request)
		{
			foreach (var vehicle in vehicles)
			{
				var valueGroups = request.AllTimeFilter ? vehicle.VehicleValues.GroupBy(i => i.EntryDate.ToString("MM. yyyy.")) :
														  vehicle.VehicleValues.Where(i => i.EntryDate >= Clock.GetTime().AddMonths(-request.NumberOfMonths ?? 0)).GroupBy(p => p.EntryDate.ToString("dd. MM. yyyy."));
				var previousBalance = 0m;
				foreach (var group in valueGroups)
				{
					var lastValue = group.OrderByDescending(b => b.EntryDate).FirstOrDefault()?.Amount ?? 0;
					result[group.Key].Assets += lastValue - previousBalance;
					previousBalance = lastValue;
				}
			}
		}
		private static void CalculatePropertyValues(Dictionary<string, GetSummaryViewModel> result, IEnumerable<Property> properties, GetDashboardChartData.Query request)
		{
			foreach (var property in properties)
			{
				var valueGroups = request.AllTimeFilter ? property.PropertyValues.GroupBy(i => i.EntryDate.ToString("MM. yyyy.")) :
														  property.PropertyValues.Where(i => i.EntryDate >= Clock.GetTime().AddMonths(-request.NumberOfMonths ?? 0)).GroupBy(p => p.EntryDate.ToString("dd. MM. yyyy."));
				var previousBalance = 0m;
				foreach (var group in valueGroups)
				{
					var lastValue = group.OrderByDescending(b => b.EntryDate).FirstOrDefault()?.Amount ?? 0;
					result[group.Key].Assets += lastValue - previousBalance;
					previousBalance = lastValue;
				}
			}
		}
		private void CalculateAccountBalances(Dictionary<string, GetSummaryViewModel> result, IEnumerable<Account> accounts, GetDashboardChartData.Query request)
		{
			foreach (var account in accounts)
			{
				var balanceGroups = request.AllTimeFilter ? account.AccountBalances.GroupBy(i => i.EntryDate.ToString("MM. yyyy.")) :
															account.AccountBalances.Where(i => i.EntryDate >= Clock.GetTime().AddMonths(-request.NumberOfMonths ?? 0)).GroupBy(p => p.EntryDate.ToString("dd. MM. yyyy."));
				var previousAssetBalance = 0m;
				var previousLiabiliteBalance = 0m;
				foreach (var group in balanceGroups)
				{
					var lastBalance = _accountService.GetLastBalance(group, account.Type);
					var lastBalanceCreditDebitIndicator = lastBalance != null ? lastBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;
					var lastBalanceAmount = lastBalance != null ? lastBalance.Amount : 0;

					var subType = AccountSubType.Parse(account.Type, lastBalanceCreditDebitIndicator == CreditDebitIndicator.Credit ? lastBalanceAmount : lastBalanceAmount * (-1));

					if (subType == AccountSubType.Asset)
					{
						result[group.Key].Assets += lastBalanceAmount - previousAssetBalance;
						previousAssetBalance = lastBalanceAmount;
					}
					else
					{
						result[group.Key].Liabilities += lastBalanceAmount - previousLiabiliteBalance;
						previousLiabiliteBalance = lastBalanceAmount;
					}
				}
			}
		}
		private static void CalculateInvestmentValues(Dictionary<string, GetSummaryViewModel> result, IEnumerable<Investment> investments, GetDashboardChartData.Query request)
		{
			foreach (var investment in investments)
			{
				var valueGroups = request.AllTimeFilter ? investment.InvestmentValues.GroupBy(i => i.EntryDate.ToString("MM. yyyy.")) :
														  investment.InvestmentValues.Where(i => i.EntryDate >= Clock.GetTime().AddMonths(-request.NumberOfMonths ?? 0)).GroupBy(p => p.EntryDate.ToString("dd. MM. yyyy."));

				var previousBalance = 0m;
				foreach (var group in valueGroups)
				{
					var lastValue = group.OrderByDescending(b => b.EntryDate).FirstOrDefault()?.Amount ?? 0;
					result[group.Key].Assets += lastValue - previousBalance;
					previousBalance = lastValue;
				}
			}
		}
		private static void CalculatePersonalItemValues(Dictionary<string, GetSummaryViewModel> result, IEnumerable<PersonalItem> personalItems, GetDashboardChartData.Query request)
		{

			foreach (var personalItem in personalItems)
			{
				var valueGroups = request.AllTimeFilter ? personalItem.PersonalItemValues.GroupBy(i => i.EntryDate.ToString("MM. yyyy.")) :
														  personalItem.PersonalItemValues.Where(i => i.EntryDate >= Clock.GetTime().AddMonths(-request.NumberOfMonths ?? 0)).GroupBy(p => p.EntryDate.ToString("dd. MM. yyyy."));

				var previousBalance = 0m;
				foreach (var group in valueGroups)
				{
					var lastValue = group.OrderByDescending(b => b.EntryDate).FirstOrDefault()?.Amount ?? 0;
					result[group.Key].Assets += lastValue - previousBalance;
					previousBalance = lastValue;
				}
			}
		}
	}
}
