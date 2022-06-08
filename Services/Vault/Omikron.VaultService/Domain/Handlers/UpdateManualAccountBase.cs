using AutoMapper;
using MediatR;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public abstract class UpdateManualAccountBase<TRequest> : BaseHandlerWithAutoMapper<TRequest, ApiResult> where TRequest : IRequest<ApiResult>
    {
        private readonly IVaultItemRepository _vaultItemRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountBalanceRepository _accountBalanceRepository;

        protected UpdateManualAccountBase(IMapper mapper, IDispatcher dispatcher, IVaultItemRepository vaultItemRepository, ITransactionRepository transactionRepository, IAccountBalanceRepository accountBalanceRepository) : base(mapper, dispatcher)
        {
            _vaultItemRepository = vaultItemRepository;
            _transactionRepository = transactionRepository;
            _accountBalanceRepository = accountBalanceRepository;
        }

        protected async Task<bool> UpdateVaultItem(Guid id, decimal amount, string itemName, string imageUrl, CancellationToken cancellationToken)
        {
            var item = await _vaultItemRepository.GetVaultItemByHostId(id, cancellationToken);
            if (item == null)
            {
                return false;
            }

            item.Value = amount;
            item.Name = itemName;
            item.ImageUrl = imageUrl;

            _vaultItemRepository.Update(item);
            return true;
        }

        protected async Task FactoryCreateAccountTransaction(Account account, decimal financeBalance, CancellationToken cancellationToken)
        {
            var accountSubType = AccountSubType.Parse(account.Type, financeBalance);
            var accountBalance = new AccountBalance()
            {
                AccountId = account.Id,
                BalanceType = Constants.PrimaryBalanceType,
                Amount = financeBalance,
                CreditDebitIndicator = accountSubType == AccountSubType.Asset ? CreditDebitIndicator.Credit : CreditDebitIndicator.Debit,
                EntryDate = Clock.GetTime()
            };

            var oldFinanceBalance = await _accountBalanceRepository.GetLatestManualAccountBalance(account.Id, cancellationToken);

            _accountBalanceRepository.Create(accountBalance);

            var transaction = new Transaction()
            {
                AccountId = account.Id,
                Category = "Value Change",
                Amount = financeBalance - oldFinanceBalance,
                Date = Clock.GetTime(),
                Currency = Constants.DefaultCurrencyCode
            };

            _transactionRepository.Create(transaction);
        }
    }
}