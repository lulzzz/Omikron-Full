using System;
using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.VaultService.Domain.Handlers
{
    public class UpdatePropertyDetailsCommandHandler : UpdateManualAccountBase<UpdateProperty.Command>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyValueRepository _propertyValueRepository;

        public UpdatePropertyDetailsCommandHandler(IMapper mapper, IDispatcher dispatcher, IPropertyRepository propertyRepository, IPropertyValueRepository propertyValueRepository, ITransactionRepository transactionRepository, IVaultItemRepository vaultItemRepository, IAccountBalanceRepository accountBalanceRepository) : base(mapper, dispatcher, vaultItemRepository, transactionRepository, accountBalanceRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyValueRepository = propertyValueRepository;
        }

        public override async Task<ApiResult> Handle(UpdateProperty.Command request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetProperty(request.PropertyId, cancellationToken);
            if (property == null)
            {
                return ApiResult.BadRequest();
            }

            var propertyExits = await _propertyRepository.PropertyExists(property.OwnerId, request.PropertyName, cancellationToken, property.Id);
            if (propertyExits)
            {
                return ApiResult.BadRequest($"Property with name {request.PropertyName} already exists. Please try a different name.");
            }

            var updatedProperty = Mapper.Map(request, property);

            if (request.PropertyValueChange)
            {
                FactoryCreateVehicleValue(request.PropertyValue, property.Id);
            }

            if (property.Mortgage != null)
            {
                property.Mortgage = Mapper.Map(request, property.Mortgage);

                if (request.MortgageBalanceChange)
                {
                    await FactoryCreateAccountTransaction(property.Mortgage, request.NewFinanceBalance, cancellationToken);
                }

                await UpdateVaultItem(property.MortgageId.Value, request.NewFinanceBalance, request.FinanceAgreementName, request.PropertyPhoto, cancellationToken);
            }

            if (!await UpdateVaultItem(property.Id, request.PropertyValue, request.PropertyName, request.PropertyPhoto, cancellationToken))
            {
                return ApiResult.BadRequest();
            }

            _propertyRepository.Update(updatedProperty);
            return await _propertyRepository.SaveChangesAsync(cancellationToken)
                ? ApiResult.Success()
                : ApiResult.BadRequest();
        }

        private void FactoryCreateVehicleValue(decimal amount, Guid propertyId)
        {
            var vehicleValue = new PropertyValue()
            {
                CreatedAt = Clock.GetTime(),
                EntryDate = Clock.GetTime(),
                PropertyId = propertyId,
                Amount = amount
            };

            _propertyValueRepository.Create(vehicleValue);
        }
    }
}