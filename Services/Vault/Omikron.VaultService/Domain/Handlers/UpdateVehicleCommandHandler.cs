using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class UpdateVehicleCommandHandler : UpdateManualAccountBase<UpdateVehicle.Command>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleValueRepository _vehicleValueRepository;

        public UpdateVehicleCommandHandler(IMapper mapper, IDispatcher dispatcher, 
            IVehicleRepository vehicleRepository, IVehicleValueRepository vehicleValueRepository, 
            ITransactionRepository transactionRepository, IVaultItemRepository vaultItemRepository, 
            IAccountBalanceRepository accountBalanceRepository)
            : base(mapper, dispatcher, vaultItemRepository, transactionRepository, accountBalanceRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleValueRepository = vehicleValueRepository;
        }

        public override async Task<ApiResult> Handle(UpdateVehicle.Command request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicle(request.VehicleId, cancellationToken);
            if (vehicle == null)
            {
                return ApiResult.BadRequest();
            }

            var vehicleExits = await _vehicleRepository.VehicleExists(vehicle.OwnerId, request.VehicleName, cancellationToken, vehicle.Id);
            if (vehicleExits)
            {
                return ApiResult.BadRequest($"Vehicle with name {request.VehicleName} already exists. Please try a different name.");
            }

            Mapper.Map(request, vehicle);

            if (request.VehicleValueChanged)
            {
                FactoryCreateVehicleValue(request.VehicleValue, request.VehicleId);
            }

            if (vehicle.FinancialAgreement != null)
            {
                vehicle.FinancialAgreement = Mapper.Map(request, vehicle.FinancialAgreement);
                if (request.FinanceBalanceChanged)
                {
                    await FactoryCreateAccountTransaction(vehicle.FinancialAgreement, request.NewFinanceBalance, cancellationToken);
                }

                await UpdateVaultItem(vehicle.FinancialAgreementId.Value, request.NewFinanceBalance, request.FinanceAgreementName, request.VehiclePhoto, cancellationToken);
            }

            if (!await UpdateVaultItem(vehicle.Id, request.VehicleValue, request.VehicleName, request.VehiclePhoto, cancellationToken))
            {
                return ApiResult.BadRequest();
            }

            _vehicleRepository.Update(vehicle);
            return await _vehicleRepository.SaveChangesAsync(cancellationToken)
                ? ApiResult.Success()
                : ApiResult.BadRequest();
        }

        private void FactoryCreateVehicleValue(decimal amount, Guid vehicleId)
        {
            var vehicleValue = new VehicleValue
            {
                CreatedAt = Clock.GetTime(),
                EntryDate = Clock.GetTime(),
                VehicleId = vehicleId,
                Amount = amount
            };

            _vehicleValueRepository.Create(vehicleValue);
        }
    }
}