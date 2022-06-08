using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class AddPropertyCommandHandler : BaseHandlerWithAutoMapper<AddProperty.Command, ApiResult>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IPropertyRepository _propertyRepository;
		private readonly IVaultItemRepository _vaultItemRepository;
		private readonly IAccountBalanceRepository _accountBalanceRepository;
		private readonly IAccountService _accountService;
		private readonly IPropertyValueRepository _propertyValueRepository;

		public AddPropertyCommandHandler(
			IAccountRepository accountRepository,
			IDispatcher dispatcher,
			IPropertyRepository propertyRepository,
			IVaultItemRepository vaultItemRepository,
			IMapper mapper,
			IAccountBalanceRepository accountBalanceRepository,
			IAccountService accountService,
			IPropertyValueRepository propertyValueRepository) : base(mapper, dispatcher)
		{
			_accountRepository = accountRepository;
			_propertyRepository = propertyRepository;
			_vaultItemRepository = vaultItemRepository;
			_accountBalanceRepository = accountBalanceRepository;
			_accountService = accountService;
			_propertyValueRepository = propertyValueRepository;
		}

		public override async Task<ApiResult> Handle(AddProperty.Command request, CancellationToken cancellationToken)
		{
			var propertyExits = await _propertyRepository.PropertyExists(request.UserId, request.PropertyName, cancellationToken);
			if (propertyExits)
			{
				return ApiResult.BadRequest($"Property with name {request.PropertyName} already exists. Please try a different name.");
			}

			var property = FactoryProperty(request);

			if (request.ExistingMortgageId.HasValue)
			{
				property.MortgageId = request.ExistingMortgageId.Value;
				var mortgage = await _accountRepository.GetAccount(request.ExistingMortgageId.Value, cancellationToken);
				mortgage.LoanType = LoanType.Mortgage;
			}

			if (request.Mortgage != null)
			{
				await FactoryMortgage(request, property, cancellationToken);
			}

			var propertyValues = FactoryPropertyValues(request, property);

			FactoryVaultItem(property, request.PropertyValue);

			_propertyRepository.Create(property);
			await _propertyValueRepository.AddRangeAsync(propertyValues, cancellationToken);
			await _propertyRepository.SaveAsync(cancellationToken);

			return ApiResult.Success();
		}

		private Property FactoryProperty(AddProperty.Command request)
		{
			var propertyId = Guid.NewGuid();

			var property = Mapper.Map<Property>(request);

			property.Id = propertyId;
			property.OwnerId = CustomerId.Parse(request.UserId);
			property.Currency = Constants.DefaultCurrencyCode;

			property.PropertyValues = new List<PropertyValue>()
			{
				new() {Amount = request.PropertyValue, PropertyId = propertyId, EntryDate = Clock.GetTime()}
			};

			return property;
		}

		private static IEnumerable<PropertyValue> FactoryPropertyValues(AddProperty.Command command, Property property)
		{
			var currentDate = Clock.GetTime();
			var values = new List<PropertyValue>()
			{
				new()
				{
					PropertyId = property.Id,
					Amount = command.PropertyValue,
					EntryDate = currentDate
				}
			};

			if (!command.PurchaseValue.HasValue || !command.PurchaseDate.HasValue)
			{
				return values;
			}

			var monthDifference = currentDate.MonthDifference(command.PurchaseDate.Value);
			var valueTrend = monthDifference > 0 ? monthDifference.TrendOverRange(command.PurchaseValue.Value, command.PropertyValue) : decimal.Zero;

			for (var i = 0; i < monthDifference; i++)
			{
				var amount = command.PurchaseValue.Value > command.PropertyValue ? command.PurchaseValue.Value - valueTrend * i : command.PurchaseValue.Value + valueTrend * i;
				var date = command.PurchaseDate.Value.AddMonths(i);

				var value = new PropertyValue()
				{
					PropertyId = property.Id,
					Amount = amount,
					EntryDate = date
				};

				values.Add(value);
			}

			return values;
		}

		private void FactoryVaultItem(Property property, decimal amount)
		{
			var vaultItem = new VaultItem
			{
				Name = property.Name,
				HostId = property.Id,
				OwnerId = property.OwnerId,
				Value = amount,
				ItemType = VaultItemType.Property,
				ImageUrl = property.ImageUrl,
				CreditDebitIndicator = CreditDebitIndicator.Credit
			};

			_vaultItemRepository.Create(vaultItem);
		}

		private async Task FactoryMortgage(AddProperty.Command request, Property property, CancellationToken cancellationToken = default)
		{
			var mortgageId = Guid.NewGuid();
			property.Mortgage = new Account()
			{
				Id = mortgageId,
				ExternalId = mortgageId,
				OwnerId = CustomerId.Parse(request.UserId),
				Currency = Constants.DefaultCurrencyCode,
				Name = request.Mortgage.Name,
				Source = AccountSource.Manual,
				Type = AccountType.Loan,
				LoanType = LoanType.Mortgage,
				Notes = request.Mortgage.Notes,
				ReferenceNumber = request.Mortgage.Reference
			};

			property.MortgageId = property.Mortgage.Id;

			await _accountBalanceRepository.AddRangeAsync(FactoryMortgageBalances(request, property), cancellationToken);

			var accountBalance = new AccountBalance()
			{
				AccountId = property.Mortgage.Id,
				BalanceType = Constants.PrimaryBalanceType,
				Amount = request.Mortgage.Balance,
				EntryDate = Clock.GetTime(),
				CreditDebitIndicator = CreditDebitIndicator.Debit
			};

			_accountBalanceRepository.Create(accountBalance);

			var vaultItem = new VaultItem()
			{
				OwnerId = CustomerId.Parse(request.UserId),
				ItemType = VaultItemType.Account,
				HostId = property.Mortgage.Id,
				Name = property.Mortgage.Name,
				Value = request.Mortgage.Balance,
				AccountType = AccountType.Loan,
				AccountSource = AccountSource.Manual,
				CreditDebitIndicator = CreditDebitIndicator.Debit
			};

			_vaultItemRepository.Create(vaultItem);
		}

		private IEnumerable<AccountBalance> FactoryMortgageBalances(AddProperty.Command command, Property property)
		{
			var currentDate = Clock.GetTime();
			var balances = new List<AccountBalance>()
			{
				new ()
				{
					AccountId = property.Mortgage.Id,
					BalanceType = Constants.PrimaryBalanceType,
					Amount = command.Mortgage.Balance,
					EntryDate = currentDate,
					CreditDebitIndicator = CreditDebitIndicator.Debit
				}
			};

			if (!command.Mortgage.OpenBalance.HasValue || !command.Mortgage.OpenDate.HasValue)
			{
				return balances;
			}

			_accountService.FactoryAccountBalanceHistory(property.Mortgage.Id,
														 AccountType.Loan,
														 balances,
														 currentDate,
														 command.Mortgage.OpenDate.Value,
														 command.Mortgage.Balance,
														 command.Mortgage.OpenBalance.Value);

			return balances;
		}
	}
}