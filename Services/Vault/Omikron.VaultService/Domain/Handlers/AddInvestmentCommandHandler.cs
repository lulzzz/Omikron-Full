using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class AddInvestmentCommandHandler : BaseHandler<AddInvestment.Command, ApiResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IVaultItemRepository _vaultItemRepository;

        public AddInvestmentCommandHandler(IDispatcher dispatcher, IAccountRepository accountRepository, IInvestmentRepository investmentRepository, IVaultItemRepository vaultItemRepository) : base(dispatcher)
        {
            _accountRepository = accountRepository;
            _investmentRepository = investmentRepository;
            _vaultItemRepository = vaultItemRepository;
        }

        public override async Task<ApiResult> Handle(AddInvestment.Command command, CancellationToken cancellationToken)
        {
            var investmnetExits = await _investmentRepository.InvestmentExists(CustomerId.Parse(command.OwnerId), command.InvestmentName, cancellationToken);
            if (investmnetExits)
            {
                return ApiResult.BadRequest($"Investment with name {command.InvestmentName} already exists. Please try a different name.");
            }

            FactoryInvestment(command);
            FactoryInvestmentVaultItem(command);

            await _accountRepository.SaveChangesAsync(cancellationToken);

            return ApiResult.Success();
        }

        private void FactoryInvestment(AddInvestment.Command command)
        {
            var investment =  new Investment
            {
                Id = command.Id,
                ExternalId = command.Id,
                Quantity = command.Quantity,
                Category = command.Category,
                UnitPrice = command.UnitPrice,
                TickerCode = command.TickerCode,
                TotalValue = command.TotalValue,
                Name = command.InvestmentName,
                Currency = Constants.DefaultCurrencyCode,
                OwnerId = CustomerId.Parse(command.OwnerId),
                AutomaticallyRevalueInvestment = command.AutomaticallyRevalueInvestment,
            };

            investment.InvestmentValues = FactoryInvestmentValues(command, investment);


            _investmentRepository.Create(investment);
        }

        private static IEnumerable<InvestmentValue> FactoryInvestmentValues(AddInvestment.Command command, Investment investment)
        {
            var currentDate = Clock.GetTime();
            var values = new List<InvestmentValue>()
            {
                new()
                {
                    InvestmentId = investment.Id,
                    Amount = command.TotalValue,
                    EntryDate = currentDate
                }
            };

            if (!command.PurchaseValue.HasValue || !command.PurchaseDate.HasValue)
            {
                return values;
            }

            var monthDifference = currentDate.MonthDifference(command.PurchaseDate.Value);
            var valueTrend = monthDifference > 0 ? monthDifference.TrendOverRange(command.PurchaseValue.Value, command.TotalValue) : decimal.Zero;

            for (var i = 0; i < monthDifference; i++)
            {
                var amount = command.PurchaseValue.Value > command.TotalValue ? command.PurchaseValue.Value - valueTrend * i : command.PurchaseValue.Value + valueTrend * i;
                var date = command.PurchaseDate.Value.AddMonths(i);

                var value = new InvestmentValue()
                {
                    InvestmentId = investment.Id,
                    Amount = amount,
                    EntryDate = date
                };

                values.Add(value);
            }

            return values;
        }

        private void FactoryInvestmentVaultItem(AddInvestment.Command command)
        {
            var vaultItem = new VaultItem()
            {
                OwnerId = CustomerId.Parse(command.OwnerId),
                ItemType = VaultItemType.Investment,
                HostId = command.Id,
                Name = command.InvestmentName,
                Value = command.TotalValue,
                CreditDebitIndicator = CreditDebitIndicator.Credit
            };

            _vaultItemRepository.Create(vaultItem);
        }
    }
}