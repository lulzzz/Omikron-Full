using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetPersonalItemQueryHandler : BaseHandlerLight<GetPersonalItemDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IPersonalItemRepository _personalItemRepository;

        public GetPersonalItemQueryHandler(IPersonalItemRepository personalItemRepository)
        {
            _personalItemRepository = personalItemRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetPersonalItemDetails.Query request, CancellationToken cancellationToken)
        {
            var personalItem = await _personalItemRepository.GetPersonalItemWithTransactions(request.AccountId, cancellationToken);
            return personalItem;
        }
    }
}