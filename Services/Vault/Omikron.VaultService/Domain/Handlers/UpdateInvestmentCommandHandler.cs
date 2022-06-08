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
    public class UpdateInvestmentCommandHandler : UpdateManualAccountBase<UpdateInvestment.Command>
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IInvestmentValuesRepository _investmentValuesRepository;

        public UpdateInvestmentCommandHandler(IMapper mapper, IDispatcher dispatcher, IVaultItemRepository vaultItemRepository, ITransactionRepository transactionRepository, IInvestmentRepository investmentRepository, IInvestmentValuesRepository investmentValuesRepository, IAccountBalanceRepository accountBalanceRepository) : base(mapper, dispatcher, vaultItemRepository, transactionRepository, accountBalanceRepository)
        {
            _investmentRepository = investmentRepository;
            _investmentValuesRepository = investmentValuesRepository;
        }

        public override async Task<ApiResult> Handle(UpdateInvestment.Command request, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.GetInvestment(request.InvestmentId, cancellationToken);
            if (investment == null)
            {
                return ApiResult.BadRequest();
            }

            var investmentExits = await _investmentRepository.InvestmentExists(investment.OwnerId, request.InvestmentName, cancellationToken, investment.Id);
            if (investmentExits)
            {
                return ApiResult.BadRequest($"Investment with name {request.InvestmentName} already exists. Please try a different name.");
            }

            Mapper.Map(request, investment);
            if (request.InvestmentValueChanged)
            {
                FactoryInvestmentValue(request.TotalValue, investment.Id);
            }

            _investmentRepository.Update(investment);
            if (!await UpdateVaultItem(investment.Id, request.TotalValue, request.InvestmentName, null, cancellationToken))
            {
                return ApiResult.BadRequest();
            }

            return await _investmentRepository.SaveChangesAsync() ? ApiResult.Success() : ApiResult.BadRequest();
        }

        private void FactoryInvestmentValue(decimal amount, Guid investmentId)
        {
            var investmentValue = new InvestmentValue()
            {
                CreatedAt = Clock.GetTime(),
                EntryDate = Clock.GetTime(),
                InvestmentId = investmentId,
                Amount = amount
            };

            _investmentValuesRepository.Create(investmentValue);
        }
    }
}