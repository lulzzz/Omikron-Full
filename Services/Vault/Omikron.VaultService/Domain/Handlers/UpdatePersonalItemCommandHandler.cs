using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class UpdatePersonalItemCommandHandler : UpdateManualAccountBase<UpdatePersonalItem.Command>
    {
        private readonly IPersonalItemRepository _personalItemRepository;
        private readonly IPersonalItemValueRepository _personalItemValueRepository;

        public UpdatePersonalItemCommandHandler(IMapper mapper, IDispatcher dispatcher, 
            IPersonalItemRepository personalItemRepository, IPersonalItemValueRepository personalItemValueRepository, 
            ITransactionRepository transactionRepository, IVaultItemRepository vaultItemRepository, 
            IAccountBalanceRepository accountBalanceRepository, IAccountRepository accountRepository) : base(mapper, dispatcher, vaultItemRepository, transactionRepository, accountBalanceRepository)
        {
            _personalItemRepository = personalItemRepository;
            _personalItemValueRepository = personalItemValueRepository;
        }

        public override async Task<ApiResult> Handle(UpdatePersonalItem.Command request, CancellationToken cancellationToken)
        {
            var personalItem = await _personalItemRepository.FindPersonalItemWithTransactionHistoryAndFinance(request.PersonalItemId, cancellationToken);
            if (personalItem == null)
            {
                return ApiResult.BadRequest();
            }

            var personalItemExists = await _personalItemRepository.PersonalItemExists(personalItem.OwnerId, request.ItemName, cancellationToken, personalItem.Id);
            if (personalItemExists)
            {
                return ApiResult.BadRequest($"Personal item with name {request.ItemName} already exists. Please try a different name.");
            }

            Mapper.Map(request, personalItem);

            if (request.PersonalItemValueChange)
            {
                FactoryPersonalItem(personalItem.Id, request.Value);
            }

            if (personalItem.FinancialAgreement != null)
            {
                personalItem.FinancialAgreement = Mapper.Map(request, personalItem.FinancialAgreement);
                if (request.FinanceBalanceChange)
                {
                    await FactoryCreateAccountTransaction(personalItem.FinancialAgreement, request.NewFinanceBalance, cancellationToken);
                }

                await UpdateVaultItem (personalItem.FinancialAgreementId.Value, request.NewFinanceBalance, request.FinanceAgreementName, request.ItemPhoto, cancellationToken);
            }

            if (!await UpdateVaultItem(request.PersonalItemId, request.Value, request.ItemName, request.ItemPhoto, cancellationToken))
            {
                return ApiResult.BadRequest();
            }

            if(request.ItemPhoto.IsNull() && personalItem.ImageUrl.IsNotNull())
            {
                var deletePhoto = await Dispatcher.DispatchAsync(new DeleteVaultItemPhoto.Command(personalItem.ImageUrl), cancellationToken);
                if(!deletePhoto.IsSuccess)
                {
                    return ApiResult.NotFound("Failed to delete image. Please try again or contact an administrator.");
                }
            }

            return await _personalItemRepository.SaveChangesAsync(cancellationToken)
                ? ApiResult.Success()
                : ApiResult.BadRequest();
        }

        private void FactoryPersonalItem(Guid personalItemId, decimal amount)
        {
            var personalItemValue = new PersonalItemValue()
            {
                Amount = amount,
                CreatedAt = Clock.GetTime(),
                EntryDate = Clock.GetTime(),
                PersonalItemId = personalItemId
            };

            _personalItemValueRepository.Create(personalItemValue);
        }

    }
}