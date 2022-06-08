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
    public class AddVehicleCommandHandler : BaseHandler<AddVehicle.Command, ApiResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleValueRepository _vehicleValueRepository;
        private readonly IVaultItemRepository _vaultItemRepository;
        private readonly IAccountBalanceRepository _accountBalanceRepository;
		private readonly IAccountService _accountService;
		private readonly IAccountRepository _accountRepository;


        public AddVehicleCommandHandler(IDispatcher dispatcher,
            IAccountRepository accountRepository,
            IVehicleRepository vehicleRepository,
            IVaultItemRepository vaultItemRepository,
            IVehicleValueRepository vehicleValueRepository,
            IAccountBalanceRepository accountBalanceRepository,
            IAccountService accountService) : base(dispatcher)
        {
            _accountRepository = accountRepository;
            _vehicleRepository = vehicleRepository;
            _vaultItemRepository = vaultItemRepository;
            _vehicleValueRepository = vehicleValueRepository;
            _accountBalanceRepository = accountBalanceRepository;
			_accountService = accountService;
		}

        public override async Task<ApiResult> Handle(AddVehicle.Command request, CancellationToken cancellationToken)
        {
            var vehicleExits = await _vehicleRepository.VehicleExists(CustomerId.Parse(request.UserId), request.VehicleName, cancellationToken);
            if (vehicleExits)
            {
                return ApiResult.BadRequest($"Vehicle with name {request.VehicleName} already exists. Please try a different name.");
            }

            var vehicle = FactoryVehicle(request);

            if (request.ExistingFinanceAgreementId.HasValue)
            {
                vehicle.FinancialAgreementId = request.ExistingFinanceAgreementId.Value;
                var financeAgreement = await _accountRepository.GetAccount(request.ExistingFinanceAgreementId.Value, cancellationToken);
                financeAgreement.LoanType = LoanType.FinancialAgreement;
            }

            if (request.FinanceAgreement != null)
            {
                await FactoryFinanceAgreement(request, vehicle, cancellationToken);
            }

            var vehicleValues = FactoryVehicleValues(request, vehicle);
            FactoryVehicleVaultItem(request, vehicle);
    
            _vehicleRepository.Create(vehicle);
            await _vehicleValueRepository.AddRangeAsync(vehicleValues, cancellationToken);
            await _vehicleRepository.SaveAsync(cancellationToken);

            return ApiResult.Success();
        }

        private static Vehicle FactoryVehicle(AddVehicle.Command request)
        {
            var vehicleId = Guid.NewGuid();
            return new Vehicle()
            {
                Id = vehicleId,
                Registration = request.Registration,
                OwnerId = CustomerId.Parse(request.UserId),
                Currency = Constants.DefaultCurrencyCode,
                Mileage = request.Mileage.ToString(),
                Name = request.VehicleName,
                AutomaticallyReValueVehicle = request.AutomaticallyReValueVehicle,
                ImageUrl = request.VehiclePhoto,
            };
        }

        private static IEnumerable<VehicleValue> FactoryVehicleValues(AddVehicle.Command command, Vehicle vehicle)
        {
            var currentDate = Clock.GetTime();
            var values = new List<VehicleValue>()
            {
                new()
                {
                    VehicleId = vehicle.Id,
                    Amount = command.VehicleValue,
                    EntryDate = currentDate
                }
            };

            if (!command.PurchaseValue.HasValue || !command.PurchaseDate.HasValue)
            {
                return values;
            }

            var monthDifference = currentDate.MonthDifference(command.PurchaseDate.Value);
            var valueTrend = monthDifference > 0 ? monthDifference.TrendOverRange(command.PurchaseValue.Value, command.VehicleValue) : decimal.Zero;

            for (var i = 0; i < monthDifference; i++)
            {
                var amount = command.PurchaseValue.Value > command.VehicleValue ? command.PurchaseValue.Value - valueTrend * i : command.PurchaseValue.Value + valueTrend * i;
                var date = command.PurchaseDate.Value.AddMonths(i);

                var value = new VehicleValue()
                {
                    VehicleId = vehicle.Id,
                    Amount = amount,
                    EntryDate = date
                };

                values.Add(value);
            }

            return values;
        }

        private void FactoryVehicleVaultItem(AddVehicle.Command request, Vehicle vehicle)
        {
            var vaultItem = new VaultItem()
            {
                OwnerId = CustomerId.Parse(request.UserId),
                ItemType = VaultItemType.Vehicle,
                HostId = vehicle.Id,
                Name = vehicle.Name,
                Value = request.VehicleValue,
                ImageUrl = vehicle.ImageUrl,
                CreditDebitIndicator = CreditDebitIndicator.Credit
            };

            _vaultItemRepository.Create(vaultItem);
        }

        private async Task FactoryFinanceAgreement(AddVehicle.Command request, Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            var financeAgreemenId = Guid.NewGuid();
            vehicle.FinancialAgreement = new Account()
            {
                Id = financeAgreemenId,
                ExternalId = financeAgreemenId,
                OwnerId = CustomerId.Parse(request.UserId),
                Currency = Constants.DefaultCurrencyCode,
                Name = request.FinanceAgreement.Name,
                Source = AccountSource.Manual,
                Type = AccountType.Loan,
                LoanType = LoanType.FinancialAgreement,
                Notes = request.FinanceAgreement.Notes,
                ReferenceNumber = request.FinanceAgreement.Reference
            };

            vehicle.FinancialAgreementId = vehicle.FinancialAgreement.Id;
            await _accountBalanceRepository.AddRangeAsync(FactoryFinanceAgreementBalances(request, vehicle), cancellationToken);

            var accountBalance = new AccountBalance()
            {
                AccountId = vehicle.FinancialAgreement.Id,
                BalanceType = Constants.PrimaryBalanceType,
                Amount = request.FinanceAgreement.Balance,
                EntryDate = Clock.GetTime(),
                CreditDebitIndicator = CreditDebitIndicator.Debit
            };

            _accountBalanceRepository.Create(accountBalance);

            var vaultItem = new VaultItem()
            {
                OwnerId = CustomerId.Parse(request.UserId),
                ItemType = VaultItemType.Account,
                HostId = vehicle.FinancialAgreement.Id,
                Name = vehicle.FinancialAgreement.Name,
                Value = request.FinanceAgreement.Balance,
                AccountType = AccountType.Loan,
                AccountSource = AccountSource.Manual,
                CreditDebitIndicator = CreditDebitIndicator.Debit
            };

            _vaultItemRepository.Create(vaultItem);
        }

		private IEnumerable<AccountBalance> FactoryFinanceAgreementBalances(AddVehicle.Command command, Vehicle vehicle)
		{
            var currentDate = Clock.GetTime();
            var balances = new List<AccountBalance>()
            {
                new ()
                {
                    AccountId = vehicle.FinancialAgreement.Id,
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

            _accountService.FactoryAccountBalanceHistory(vehicle.FinancialAgreement.Id,
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