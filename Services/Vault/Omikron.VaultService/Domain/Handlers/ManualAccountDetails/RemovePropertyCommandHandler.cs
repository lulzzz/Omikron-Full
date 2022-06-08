using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemovePropertyCommandHandler : RemoveManualAccountBaseHandler<RemoveProperty.Command>
    {
        private readonly IPropertyRepository _propertyRepository;

        public RemovePropertyCommandHandler(IPropertyRepository propertyRepository, IVaultItemRepository vaultItemRepository) : base(vaultItemRepository)
        {
            _propertyRepository = propertyRepository;
        }

        protected override async Task<bool> ArchiveAccount(RemoveProperty.Command request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetProperty(request.AccountId, cancellationToken);
            if (property.IsNull())
            {
                return false;
            }

            property.IsArchived = true;
            await _propertyRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override async Task<bool> RemoveAccount(RemoveProperty.Command request, CancellationToken cancellationToken)
        {
            return await _propertyRepository.RemoveProperty(request.AccountId, cancellationToken);
        }
    }
}