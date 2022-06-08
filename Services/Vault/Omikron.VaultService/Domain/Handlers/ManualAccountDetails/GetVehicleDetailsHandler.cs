using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetVehicleDetailsHandler : BaseHandlerLight<GetVehicleDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleDetailsHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetVehicleDetails.Query request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.FindVehicleWithTransactionHistoryAndFinance(request.AccountId, cancellationToken);
            return vehicle;
        }
    }
}