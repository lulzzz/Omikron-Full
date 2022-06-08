using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetInvestmentDetailsQueryHandler : BaseHandlerLight<GetInvestmentDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IInvestmentRepository _investmentRepository;

        public GetInvestmentDetailsQueryHandler(IInvestmentRepository investmentRepository) 
        {
            _investmentRepository = investmentRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetInvestmentDetails.Query request, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.GetInvestmentWithTransactionHistory(request.AccountId, cancellationToken);
            return investment;
        }
    }
}