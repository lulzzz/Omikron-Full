using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Extensions;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Dashboard;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetAccountsQueryHandler : BaseHandlerLight<GetAccounts.Query, ApiResult<DashboardAccountGroupingViewModel>>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IAccountService _accountService;
		private readonly IPropertyRepository _propertyRepository;
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IPersonalItemRepository _personalItemRepository;
		private readonly IInvestmentRepository _investmentRepository;
		private readonly string _currentAccountTypeGroupKeySeparator = "-";

		public GetAccountsQueryHandler(IAccountRepository accountRepository, IAccountService accountService,
			IPropertyRepository propertyRepository, IVehicleRepository vehicleRepository,
			IPersonalItemRepository personalItemRepository, IInvestmentRepository investmentRepository)
		{
			_accountRepository = accountRepository;
			_accountService = accountService;
			_propertyRepository = propertyRepository;
			_vehicleRepository = vehicleRepository;
			_personalItemRepository = personalItemRepository;
			_investmentRepository = investmentRepository;
		}

		public override async Task<ApiResult<DashboardAccountGroupingViewModel>> Handle(GetAccounts.Query request, CancellationToken cancellationToken)
		{
			var result = new DashboardAccountGroupingViewModel();

			var accounts = await _accountRepository.GetAccountsWithBalanceByOwnerId(request.UserId, cancellationToken);
			if (!accounts.IsNullOrEmpty())
			{
				var groups = GroupAccounts(accounts);
				PopulateResultWithAccounts(result, groups);
			}

			await PopulateResultWithProperties(request, result, cancellationToken);
			await PopulateResultWithVehicles(request, result, cancellationToken);
			await PopulateResultWithPersonalItems(request, result, cancellationToken);
			await PopulateResultWithInvestments(request, result, cancellationToken);

			result.Assets.Items =  result.Assets.Items.OrderByDescending(x => x.Total);
			result.Liabilities.Items =  result.Liabilities.Items.OrderByDescending(x => x.Total);
			return ApiResult<DashboardAccountGroupingViewModel>.Success().WithData(result);
		}

		private void PopulateResultWithAccounts(DashboardAccountGroupingViewModel result, IDictionary<string, IEnumerable<Account>> groups)
		{
			result.Assets.Currency = Constants.DefaultCurrencyCode;
			result.Liabilities.Currency = Constants.DefaultCurrencyCode;

			foreach (var group in groups)
			{
				var total = _accountService.CalculateTotalBalance(group.Value);
				var groupKey = ExtractGroupKey(group.Key);
				var subType = AccountSubType.Parse(groupKey, total);

				var accountsViewModel = FactoryGetAccountViewModel(group.Value, total, groupKey);

				if (subType == AccountSubType.Asset)
				{
					result.Assets.Total += total;
					result.Assets.Items = result.Assets.Items.Append(accountsViewModel);
				}
				else
				{
					result.Liabilities.Total += total;
					result.Liabilities.Items = result.Liabilities.Items.Append(accountsViewModel);
				}
			}
		}

		private string ExtractGroupKey(string key)
		{
			var groupKeyParts = key.Split(_currentAccountTypeGroupKeySeparator);
			return groupKeyParts[0];
		}

		private async Task PopulateResultWithProperties(GetAccounts.Query request, DashboardAccountGroupingViewModel result, CancellationToken cancellationToken)
		{
			var properties = await _propertyRepository.GetPropertiesByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);

			if (!properties.Any())
			{
				return;
			}

			var propertyValuesTotal = _accountService.CalculateTotalValue(properties);
			AppendAssetToResult(result, properties, propertyValuesTotal, VaultItemType.Property);
		}

		private async Task PopulateResultWithVehicles(GetAccounts.Query request, DashboardAccountGroupingViewModel result, CancellationToken cancellationToken)
		{
			var vehicles = await _vehicleRepository.GetVehiclesByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);

			if (!vehicles.Any())
			{
				return;
			}

			var vehiclesValuesTotal = _accountService.CalculateTotalValue(vehicles);
			AppendAssetToResult(result, vehicles, vehiclesValuesTotal, VaultItemType.Vehicle);
		}

		private async Task PopulateResultWithPersonalItems(GetAccounts.Query request, DashboardAccountGroupingViewModel result, CancellationToken cancellationToken)
		{
			var personalItems = await _personalItemRepository.GetPersonalItemsByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);

			if (!personalItems.Any())
			{
				return;
			}

			var personalItemsValuesTotal = _accountService.CalculateTotalValue(personalItems);
			AppendAssetToResult(result, personalItems, personalItemsValuesTotal, VaultItemType.PersonalItem);
		}

		private async Task PopulateResultWithInvestments(GetAccounts.Query request, DashboardAccountGroupingViewModel result, CancellationToken cancellationToken)
		{
			var investments = await _investmentRepository.GetInvestmentsByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);

			if (!investments.Any())
			{
				return;
			}

			var investmentTotal = _accountService.CalculateTotalValue(investments);
			AppendAssetToResult(result, investments, investmentTotal, VaultItemType.Investment);
		}

		private static void AppendAssetToResult<T>(DashboardAccountGroupingViewModel result, IEnumerable<T> personalItems, decimal personalItemsValuesTotal, string typeName) where T : BaseVaultItem<Guid>
		{
			var accountsViewModel = FactoryGetAccountViewModel(personalItems, personalItemsValuesTotal, typeName);
			result.Assets.Total += personalItemsValuesTotal;
			result.Assets.Items = result.Assets.Items.Append(accountsViewModel);
		}

		private static GetAccountsViewModel FactoryGetAccountViewModel<T>(IEnumerable<T> personalItems, decimal personalItemsValuesTotal, string typeName) where T : BaseVaultItem<Guid>
		{
			var accountsViewModel = new GetAccountsViewModel()
			{
				// This will work if all accounts for specific AccountType are in the same currency - otherwise we will have to deal with currency conversion to one master of user specified currency
				Types = typeName.ToAccountGroupDisplayName(),
				Total = personalItemsValuesTotal,
				// Again, this will work only when all accounts for this specific AccountType are in the same currency, otherwise we will have to set one master of user specified currency
				Currency = personalItems.Select(a => a.Currency).First()
			};
			return accountsViewModel;
		}

		private IDictionary<string, IEnumerable<Account>> GroupAccounts(IEnumerable<Account> accounts)
		{
			var groups = new Dictionary<string, IEnumerable<Account>>();

			foreach (var account in accounts)
			{
				var groupKey = GetGroupKey(account);

				if (!groups.TryGetValue(groupKey, out var _))
				{
					groups.Add(groupKey, new List<Account>());
				}

				groups[groupKey] = groups[groupKey].Append(account);
			}

			return groups;
		}

		private string GetGroupKey(Account account)
		{
			if (account.Type == AccountType.ChargeCard || account.Type == AccountType.CreditCard)
			{
				return AccountType.CreditCard;
			}
			if (account.Type == AccountType.EMoney || account.Type == AccountType.PrePaidCard || account.Type == AccountType.CurrentAccount)
			{
				return GetCurrentAccountGroupKey(account);
			}
			if (account.Type == AccountType.Loan)
			{
				return GetGroupKeyFromLoanType(account);
			}

			return account.Type;
		}

		private string GetCurrentAccountGroupKey(Account account)
		{
			var groupKeyPrefix = $"{AccountType.CurrentAccount}{_currentAccountTypeGroupKeySeparator}";
			var lastBalance = _accountService.GetLastBalance(account);
			var creditDebitIndicator = lastBalance != null ? lastBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;

			return creditDebitIndicator == CreditDebitIndicator.Credit ? $"{groupKeyPrefix}{AccountSubType.Asset}" : $"{groupKeyPrefix}{AccountSubType.Liability}";
		}

		private static string GetGroupKeyFromLoanType(Account account)
		{
			return account?.LoanType?.ToString() ?? AccountType.Loan.ToString();
		}
	}
}