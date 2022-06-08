using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetPersonalItemDetailsQueryHandler : BaseHandlerLight<GetPersonalItemDetails.Query, ApiResult<PersonalItemViewModel>>
    {
        private readonly IPersonalItemRepository _personalItemRepository;

        public GetPersonalItemDetailsQueryHandler(IPersonalItemRepository personalItemRepository)
        {
            _personalItemRepository = personalItemRepository;
        }

        public override async Task<ApiResult<PersonalItemViewModel>> Handle(GetPersonalItemDetails.Query request, CancellationToken cancellationToken)
        {
            var personalItem = await _personalItemRepository.GetPersonalItemDetails(request.AccountId, cancellationToken);
            return personalItem != null ? ApiResult<PersonalItemViewModel>.Success().WithData(personalItem) : ApiResult<PersonalItemViewModel>.NotFound();
        }
    }
}