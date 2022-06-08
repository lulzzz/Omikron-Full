using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetPropertyDetailsHandler : BaseHandlerLight<GetPropertyDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertyDetailsHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetPropertyDetails.Query request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.FindPropertyWithTransactionHistory(request.AccountId, cancellationToken);
            return property;
        }
    }
}