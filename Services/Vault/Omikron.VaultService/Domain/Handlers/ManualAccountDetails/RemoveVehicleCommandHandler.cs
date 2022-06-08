using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemoveVehicleCommandHandler : RemoveManualAccountBaseHandler<RemoveVehicle.Command>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public RemoveVehicleCommandHandler(IVehicleRepository vehicleRepository, IVaultItemRepository vaultItemRepository) : base(vaultItemRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        protected override async Task<bool> ArchiveAccount(RemoveVehicle.Command request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicle(request.AccountId, cancellationToken);
            if (vehicle.IsNull())
            {
                return false;
            }

            vehicle.IsArchived = true;
            await _vehicleRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override async Task<bool> RemoveAccount(RemoveVehicle.Command request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.RemoveVehicle(request.AccountId, cancellationToken);
        }
    }
}