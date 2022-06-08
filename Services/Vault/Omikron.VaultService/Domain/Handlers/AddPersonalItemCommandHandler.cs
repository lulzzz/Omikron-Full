using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using System.Collections.Generic;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Extensions;

namespace Omikron.VaultService.Domain.Handlers
{
	public class AddPersonalItemCommandHandler : BaseHandler<AddPersonalItem.Command, ApiResult>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IVaultItemRepository _vaultItemRepository;
		private readonly IPersonalItemRepository _personalItemRepository;
		private readonly IAccountBalanceRepository _accountBalanceRepository;
		private readonly IPersonalItemValueRepository _personalItemValueRepository;
		private readonly IAccountService _accountService;

		public AddPersonalItemCommandHandler(IDispatcher dispatcher,
			IAccountRepository accountRepository,
			IVaultItemRepository vaultItemRepository,
			IPersonalItemRepository personalItemRepository,
			IAccountBalanceRepository accountBalanceRepository,
			IPersonalItemValueRepository personalItemValueRepository,
			IAccountService accountService) : base(dispatcher)
		{
			_accountRepository = accountRepository;
			_vaultItemRepository = vaultItemRepository;
			_personalItemRepository = personalItemRepository;
			_accountBalanceRepository = accountBalanceRepository;
			_personalItemValueRepository = personalItemValueRepository;
			_accountService = accountService;
		}

		public override async Task<ApiResult> Handle(AddPersonalItem.Command request, CancellationToken cancellationToken)
		{
			var personalItemExists = await _personalItemRepository.PersonalItemExists(CustomerId.Parse(request.UserId), request.ItemName, cancellationToken);
			if (personalItemExists)
			{
				return ApiResult.BadRequest($"Personal item with name {request.ItemName} already exists. Please try a different name.");
			}

			var personalItem = FactoryPersonalItem(request);

			if (request.ExistingFinanceAgreementId.HasValue)
			{
				personalItem.FinancialAgreementId = request.ExistingFinanceAgreementId.Value;
				var financeAgreement = await _accountRepository.GetAccount(request.ExistingFinanceAgreementId.Value, cancellationToken);
				financeAgreement.LoanType = LoanType.FinancialAgreement;
			}

			if (request.FinanceAgreement != null)
			{
				await FactoryFinanceAgreement(request, personalItem, cancellationToken);
			}

			var personalItemValues = FactoryPersonalItemValues(request, personalItem);
			FactoryPersonalItemVaultItem(request, personalItem);

			_personalItemRepository.Create(personalItem);
			await _personalItemValueRepository.AddRangeAsync(personalItemValues, cancellationToken);
			await _personalItemRepository.SaveAsync(cancellationToken);

			return ApiResult.Success();
		}

		private static PersonalItem FactoryPersonalItem(AddPersonalItem.Command request)
		{
			var personalItemId = Guid.NewGuid();
			return new PersonalItem()
			{
				Id = personalItemId,
				ExternalId = personalItemId,
				OwnerId = CustomerId.Parse(request.UserId),
				Currency = Constants.DefaultCurrencyCode, //TODO: Check with Omikron how we should manage currencies
				Name = request.ItemName,
				ImageUrl = request.ItemPhoto
			};
		}

		private static IEnumerable<PersonalItemValue> FactoryPersonalItemValues(AddPersonalItem.Command command, PersonalItem personalItem)
		{
			var currentDate = Clock.GetTime();
			var values = new List<PersonalItemValue>()
			{
				new()
				{
					PersonalItemId = personalItem.Id,
					Amount = command.Value,
					EntryDate = currentDate
				}
			};

			if (!command.PurchaseValue.HasValue || !command.PurchaseDate.HasValue)
			{
				return values;
			}

			var monthDifference = currentDate.MonthDifference(command.PurchaseDate.Value);
			var valueTrend = monthDifference > 0 ? monthDifference.TrendOverRange(command.PurchaseValue.Value, command.Value) : decimal.Zero;

			for (var i = 0; i < monthDifference; i++)
			{
				var amount = command.PurchaseValue.Value > command.Value ? command.PurchaseValue.Value - valueTrend * i : command.PurchaseValue.Value + valueTrend * i;
				var date = command.PurchaseDate.Value.AddMonths(i);

				var value = new PersonalItemValue()
				{
					PersonalItemId = personalItem.Id,
					Amount = amount,
					EntryDate = date
				};

				values.Add(value);
			}

			return values;
		}

		private void FactoryPersonalItemVaultItem(AddPersonalItem.Command request, PersonalItem personalItem)
		{
			var vaultItem = new VaultItem()
			{
				OwnerId = CustomerId.Parse(request.UserId),
				ItemType = VaultItemType.PersonalItem,
				HostId = personalItem.Id,
				Name = personalItem.Name,
				Value = request.Value,
				ImageUrl = personalItem.ImageUrl,
				CreditDebitIndicator = CreditDebitIndicator.Credit
			};

			_vaultItemRepository.Create(vaultItem);
		}

		private async Task FactoryFinanceAgreement(AddPersonalItem.Command request, PersonalItem personalItem, CancellationToken cancellationToken = default)
		{
			var financeAgreementId = Guid.NewGuid();
			personalItem.FinancialAgreement = new Account()
			{
				Id = financeAgreementId,
				ExternalId = financeAgreementId,
				OwnerId = CustomerId.Parse(request.UserId),
				Currency = Constants.DefaultCurrencyCode,
				Name = request.FinanceAgreement.Name,
				Source = AccountSource.Manual,
				Type = AccountType.Loan,
				LoanType = LoanType.FinancialAgreement,
				Notes = request.FinanceAgreement.Notes,
			};

			personalItem.FinancialAgreementId = personalItem.FinancialAgreement.Id;
			await _accountBalanceRepository.AddRangeAsync(FactoryFinanceAgreementBalances(request, personalItem), cancellationToken);

			var vaultItem = new VaultItem()
			{
				OwnerId = CustomerId.Parse(request.UserId),
				ItemType = VaultItemType.Account,
				HostId = personalItem.FinancialAgreement.Id,
				Name = personalItem.FinancialAgreement.Name,
				Value = request.FinanceAgreement.Balance,
				AccountType = AccountType.Loan,
				AccountSource = AccountSource.Manual,
				CreditDebitIndicator = CreditDebitIndicator.Debit
			};

			_vaultItemRepository.Create(vaultItem);
		}

		private IEnumerable<AccountBalance> FactoryFinanceAgreementBalances(AddPersonalItem.Command command, PersonalItem personalItem)
		{
			var currentDate = Clock.GetTime();
			var balances = new List<AccountBalance>()
			{
				new ()
				{
					AccountId = personalItem.FinancialAgreement.Id,
					BalanceType = Constants.PrimaryBalanceType,
					Amount = command.FinanceAgreement.Balance,
					EntryDate = currentDate,
					CreditDebitIndicator = CreditDebitIndicator.Debit
				}
			};

			if (!command.FinanceAgreement.OpenBalance.HasValue || !command.FinanceAgreement.OpenDate.HasValue)
			{
				return balances;
			}

			_accountService.FactoryAccountBalanceHistory(personalItem.FinancialAgreement.Id,
														 AccountType.Loan,
														 balances,
														 currentDate,
														 command.FinanceAgreement.OpenDate.Value,
														 command.FinanceAgreement.Balance,
														 command.FinanceAgreement.OpenBalance.Value);

			return balances;
		}
	}
}
