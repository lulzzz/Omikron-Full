using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Accumulators;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Comparers;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.Sync.Bud.Commands;

namespace Omikron.Sync.Service.Business.Handlers
{
	public class SyncAccountsCommandHandler : BaseHandlerLight<SyncAccounts.Command, ApiResult>
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IVaultItemRepository _vaultItemRepository;
		private readonly IAccountService _accountService;
		private readonly LoggerContext _logger;

		public SyncAccountsCommandHandler(IAccountRepository accountRepository, IVaultItemRepository vaultItemRepository, IAccountService accountService, LoggerContext logger)
		{
			_accountRepository = accountRepository;
			_vaultItemRepository = vaultItemRepository;
			_accountService = accountService;
			_logger = logger;
		}

		public override async Task<ApiResult> Handle(SyncAccounts.Command request, CancellationToken cancellationToken)
		{
			var accounts = new List<Account>();

			// This funcitonalitiy is used because "PopulateConsents" is not available for Bud API prod env
			var consentsExpiryDates = GetLastConsents(request);

			// This should be used once "PopulateConsents" is available for use
			//var consentsExpiryDates = new Dictionary<string, DateTime>();
			//PopulateConsents(request, consentsExpiryDates);

			PopulateAccounts(request, accounts, consentsExpiryDates);

			var existingUserBudAccounts = await _accountRepository.GetUserBudAccounts(ownerId: CustomerId.Parse(id: request.UserId), cancellationToken: cancellationToken);

			var budAccountsToRemove = existingUserBudAccounts.Where(predicate: a => !accounts.Contains(value: a, comparer: new AccountsByBudAccountIdComparer()) && a.Source == AccountSource.BudApi);
			var accountsToAddOrUpdate = accounts.Aggregate(seed: new AccountsAccumulator(existingAccounts: existingUserBudAccounts),
				func: (acc, a) => acc.Accumulate(account: a),
				resultSelector: acc => acc);

			if (accountsToAddOrUpdate.AccountsToAdd.Any())
			{
				await _accountRepository.AddRangeAsync(entities: accountsToAddOrUpdate.AccountsToAdd, cancellationToken: cancellationToken);
				await AddAccountVaultItems(request.UserId, accountsToAddOrUpdate.AccountsToAdd, cancellationToken);
			}

			if (accountsToAddOrUpdate.AccountsToUpdate.Any())
			{
				_accountRepository.UpdateRange(entities: accountsToAddOrUpdate.AccountsToUpdate);
				await UpdateAccountVaultItems(accountsToAddOrUpdate.AccountsToUpdate, cancellationToken);
			}

			if (budAccountsToRemove.Any())
			{
				_accountRepository.RemoveRange(entities: budAccountsToRemove);
				await RemoveAccountVaultItems(budAccountsToRemove, cancellationToken);
			}

			await _accountRepository.SaveChangesAsync(cancellationToken: cancellationToken);

			return ApiResult.Success();
		}

		private async Task RemoveAccountVaultItems(IEnumerable<Account> budAccountsToRemove, CancellationToken cancellationToken)
		{
			var vaultItemsToRemove = await _vaultItemRepository.GetVaultItemsByHostIds(budAccountsToRemove.Select(a => a.ExternalId), cancellationToken);
			_vaultItemRepository.RemoveRange(vaultItemsToRemove);
		}

		private async Task UpdateAccountVaultItems(List<Account> accountsToUpdate, CancellationToken cancellationToken)
		{
			var vaultItemsToUpdate = await _vaultItemRepository.GetVaultItemsByHostIds(accountsToUpdate.Select(a => a.ExternalId), cancellationToken);
			foreach (var vaultItem in vaultItemsToUpdate)
			{
				var correspondingAccount = accountsToUpdate.FirstOrDefault(a => a.ExternalId == vaultItem.HostId);
				var accountBalance = _accountService.GetLastBalance(correspondingAccount);
				var lastBalanceAmount = accountBalance != null ? accountBalance.Amount : 0;
				var lastBalanceCreditDebitIndicator = accountBalance != null ? accountBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;

				vaultItem.AccountProvider = correspondingAccount.Provider;
				vaultItem.AccountIdentificationNumber = correspondingAccount.IdentificationNumber;
				vaultItem.Name = correspondingAccount.Name;
				vaultItem.Value = lastBalanceAmount;
				vaultItem.CreditDebitIndicator = lastBalanceCreditDebitIndicator;
				vaultItem.AccountSource = correspondingAccount.Source;
				vaultItem.AccountExpiryDate = correspondingAccount.ExpiryDate;
				vaultItem.AccountType = correspondingAccount.Type;
				vaultItem.ReferenceNumber = correspondingAccount.ReferenceNumber;
			}
			_vaultItemRepository.UpdateRange(vaultItemsToUpdate);
		}

		private async Task AddAccountVaultItems(Guid ownerId, List<Account> accountsToAdd, CancellationToken cancellationToken)
		{
			var vaultItems = accountsToAdd.Select(a =>
			{
				var accountBalance = _accountService.GetLastBalance(a);
				var lastBalanceAmount = accountBalance != null ? accountBalance.Amount : 0;
				var lastBalanceCreditDebitIndicator = accountBalance != null ? accountBalance.CreditDebitIndicator : CreditDebitIndicator.Credit;

				return new VaultItem()
				{
					OwnerId = CustomerId.Parse(ownerId),
					ItemType = VaultItemType.Account,
					AccountType = a.Type,
					HostId = a.ExternalId,
					AccountProvider = a.Provider,
					AccountIdentificationNumber = a.IdentificationNumber,
					Name = a.Name,
					Value = lastBalanceAmount,
					CreditDebitIndicator = lastBalanceCreditDebitIndicator,
					AccountExpiryDate = a.ExpiryDate,
					ReferenceNumber = a.ReferenceNumber,
					AccountSource = a.Source,
				};
			});

			await _vaultItemRepository.AddRangeAsync(vaultItems, cancellationToken);
		}

		private void PopulateAccounts(SyncAccounts.Command request, List<Account> accounts, Dictionary<string, DateTime> consentsExpiryDates)
		{
			foreach (var budAccount in request.BudAccounts)
			{
				// This condition should be used when "PopulateConsents" is used
				//if (!consentsExpiryDates.TryGetValue(budAccount.AccountId, out var expiryDate) || budAccount.Currency != Constants.DefaultCurrencyCode)
				if (!consentsExpiryDates.TryGetValue(budAccount.Provider, out var expiryDate) || budAccount.Currency != Constants.DefaultCurrencyCode)
				{
					continue;
				}

				var account = new Account
				{
					OwnerId = CustomerId.Parse(id: request.UserId),
					Currency = budAccount.Currency,
					Name = budAccount.Nickname,
					Source = AccountSource.BudApi,
					Provider = budAccount.Provider,
					BudAccountId = budAccount.AccountId,
					ExpiryDate = expiryDate,
				};

				SetIdentificationNumber(budAccount, account);
				SetAccountType(accountSubType: budAccount.AccountSubType, account: account);

				var balances = budAccount.Balances.Select(selector: b => new AccountBalance
				{
					BudAccountId = b.AccountId,
					BalanceType = BalanceType.Parse(value: b.Type),
					Amount = b.Amount.Amount.ToDecimal(),
					EntryDate = b.DateTime,
					CreditDebitIndicator = b.CreditDebitIndicator
				});

				account.AccountBalances = new List<AccountBalance>(collection: balances);

				accounts.Add(item: account);
			}
		}

		private void SetIdentificationNumber(BudListAccountsResponse budAccount, Account account)
		{
			var sortCodeAccountNumber = budAccount.Details.FirstOrDefault(d => d.SchemeName == Constants.SortCodeAccountNumberSchemeName);
			if (sortCodeAccountNumber != null)
			{
				account.IdentificationNumber = new AccountIdentificationNumber(sortCodeAccountNumber.Identification);
				return;
			}

			var panNumber = budAccount.Details.FirstOrDefault(d => d.SchemeName == Constants.PanSchemeName);
			if (panNumber != null)
			{
				account.IdentificationNumber = panNumber.Identification.IsNotNullOrEmpty() && panNumber.Identification.Length >= Constants.NumberOfKnownDigitsOfPanNumber
					? new AccountIdentificationNumber(panNumber.Identification[^Constants.NumberOfKnownDigitsOfPanNumber..])
					: new AccountIdentificationNumber(panNumber.Identification);
				return;
			}

			_logger.UsageLogger.Information($"Missing identification number for account with Bud id: {budAccount.AccountId} - Provider: {budAccount.Provider} - Name: {budAccount.Nickname}");
		}

		// This is used because the "PopulateConsents" method cannot be used in Bud API Prod env, yet
		private static Dictionary<string, DateTime> GetLastConsents(SyncAccounts.Command request)
		{
			return request.CustomerConsents.GroupBy(p => p.Provider).Select(g => g.OrderByDescending(p => p.LastConsentedOn).FirstOrDefault()).ToDictionary(p => p.Provider, p => p.ExpirationDate);
		}

		// Use this funcionallity once it becomes available on Bud API Prod env
		private static void PopulateConsents(SyncAccounts.Command request, Dictionary<string, DateTime> consentsExpiryDates)
		{
			foreach (var consent in request.CustomerConsents)
			{
				foreach (var accountId in consent.AccountIds)
				{
					if (consentsExpiryDates.TryGetValue(accountId, out var expirationDate))
					{
						if (consent.ExpirationDate > expirationDate)
						{
							consentsExpiryDates[accountId] = consent.ExpirationDate;
						}
					}
					else
					{
						consentsExpiryDates.Add(accountId, consent.ExpirationDate);
					}
				}
			}
		}

		private static void SetAccountType(string accountSubType, Account account)
		{
			if (accountSubType == BudSpecificAccountType.Mortgage)
			{
				account.Type = AccountType.Loan;
				account.LoanType = LoanType.Mortgage;
			}
			else
			{
				account.Type = AccountType.Parse(value: accountSubType);
			}
		}
	}
}