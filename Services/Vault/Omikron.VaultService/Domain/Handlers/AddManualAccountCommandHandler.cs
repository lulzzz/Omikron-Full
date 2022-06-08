using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using System.Collections.Generic;
using System;
using Omikron.SharedKernel.Infrastructure.Vault.Services;

namespace Omikron.VaultService.Domain.Handlers
{
    public class AddManualAccountCommandHandler : BaseHandler<AddManualAccount.Command, ApiResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
		private readonly IAccountService _accountService;
		private readonly IVaultItemRepository _vaultItemRepository;

        public AddManualAccountCommandHandler(IDispatcher dispatcher,
                                              IAccountRepository accountRepository,
                                              IVaultItemRepository vaultItemRepository,
                                              IAccountBalanceRepository accountBalanceRepository,
											  IAccountService accountService) : base(dispatcher)
        {
            _accountRepository = accountRepository;
            _vaultItemRepository = vaultItemRepository;
            _accountBalanceRepository = accountBalanceRepository;
			_accountService = accountService;
		}

        public override async Task<ApiResult> Handle(AddManualAccount.Command command, CancellationToken cancellationToken)
        {
            var accountExits = await _accountRepository.AccountExists(CustomerId.Parse(command.OwnerId), command.Name, cancellationToken);
            if(accountExits)
            {
                return ApiResult.BadRequest($"Account with name {command.Name} already exists. Please try a different name.");
            }

            var account = PopulateAccount(command);
            var accountBalances = PopulateAccountBalances(command, account);
            var vaultItem = PopulateVaulItem(account, GetLastBalance(command, account));

            _accountRepository.Create(account);
            await _accountBalanceRepository.AddRangeAsync(accountBalances, cancellationToken);
            _vaultItemRepository.Create(vaultItem);

            var saveSuccessful = await _accountRepository.SaveChangesAsync(cancellationToken);

            return saveSuccessful ? ApiResult.Success() : ApiResult.BadRequest("We could not add your account at this time. Please try again later.");
        }

        private static Account PopulateAccount(AddManualAccount.Command command)
        {
            var account =  new Account
            {
                Id = command.Id,
                Name = command.Name,
                Notes = command.Notes,
                ExternalId = command.Id,
                Source = AccountSource.Manual,
                Type = AccountType.Parse(command.Type),
                OwnerId = CustomerId.Parse(command.OwnerId),
                ReferenceNumber = command.ReferenceNumber,
                Currency = Constants.DefaultCurrencyCode,
            };

			return account;
        }
        
        private IEnumerable<AccountBalance> PopulateAccountBalances(AddManualAccount.Command command, Account account)
		{
			var balances = new List<AccountBalance>();
			balances.Add(GetLastBalance(command, account));

			if (!command.OpenBalance.HasValue || !command.OpenDate.HasValue)
			{
				return balances;
			}

			FactoryHistoryBalances(command, account, balances);

			return balances;
		}

		private void FactoryHistoryBalances(AddManualAccount.Command command, Account account, List<AccountBalance> balances)
		{
			var currentDate = Clock.GetTime();
			var openDate = command.OpenDate.Value;
			var currentBalance = command.CreditDebitIndicator.Value == CreditDebitIndicator.Credit ? command.Balance : command.Balance * (-1);

            var openBalance = command.Type == AccountType.CurrentAccount.Id ? command.OpenBalance.Value : Math.Abs(command.OpenBalance.Value);

            _accountService.FactoryAccountBalanceHistory(command.Id, AccountType.Parse(command.Type), balances, currentDate, openDate, currentBalance, openBalance);
		}

		private static AccountBalance GetLastBalance(AddManualAccount.Command command, Account account)
		{
			var lastBalance = new AccountBalance
			{
				Amount = command.Balance,
				AccountId = command.Id,
				EntryDate = Clock.GetTime(),
				BalanceType = Constants.PrimaryBalanceType,
			};

			if (account.Type == AccountType.CurrentAccount)
			{
				lastBalance.CreditDebitIndicator = CreditDebitIndicator.Parse(command.CreditDebitIndicator.Value);
			}
			else
			{
				var accountSubType = AccountSubType.Parse(account.Type);
				lastBalance.CreditDebitIndicator = accountSubType == AccountSubType.Asset ? CreditDebitIndicator.Credit : CreditDebitIndicator.Debit;
			}

			return lastBalance;
		}

		private static VaultItem PopulateVaulItem(Account account, AccountBalance accountBalance)
        {
            return new VaultItem
            {
                Name = account.Name,
                HostId = account.Id,
                OwnerId = account.OwnerId,
                AccountType = account.Type,
                Value = accountBalance.Amount,
                CreditDebitIndicator = accountBalance.CreditDebitIndicator,
                ItemType = VaultItemType.Account,
                AccountProvider = account.Provider,
                ReferenceNumber = account.ReferenceNumber,
                AccountSource = account.Source
            };
        }
    }
}