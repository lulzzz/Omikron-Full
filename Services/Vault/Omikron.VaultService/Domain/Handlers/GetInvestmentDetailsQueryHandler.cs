using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetInvestmentDetailsQueryHandler : BaseHandlerLight<GetInvestmentDetails.Query, ApiResult<InvestmentViewModel>>
    {
        private readonly IInvestmentRepository _investmentRepository;

        public GetInvestmentDetailsQueryHandler(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        public override async Task<ApiResult<InvestmentViewModel>> Handle(GetInvestmentDetails.Query request, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.GetInvestmentDetails(request.AccountId, cancellationToken);
            return investment == null
                ? ApiResult<InvestmentViewModel>.NotFound()
                : ApiResult<InvestmentViewModel>.Success().WithData(investment);
        }
    }
}