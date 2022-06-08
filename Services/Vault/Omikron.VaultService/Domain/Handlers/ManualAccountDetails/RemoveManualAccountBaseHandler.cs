using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public abstract class RemoveManualAccountBaseHandler<TRequest> : BaseHandlerLight<TRequest, bool> where TRequest : RemoveManualAccountBase
    {
        private readonly IVaultItemRepository _vaultItemRepository;

        protected RemoveManualAccountBaseHandler(IVaultItemRepository vaultItemRepository)
        {
            _vaultItemRepository = vaultItemRepository;
        }

        public override async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if(request.IsArchived)
            {
                var archiveAccount = await ArchiveAccount(request, cancellationToken);
                if (!archiveAccount)
                {
                    return false;
                }
            }
            else
            {
                var removeAccount = await RemoveAccount(request, cancellationToken);
                if (!removeAccount)
                {
                    return false;
                }
            }

            var removeVaultItem = await _vaultItemRepository.DeleteManualItem(request.AccountId, cancellationToken);
            if (!removeVaultItem)
            {
                return false;
            }

            return await _vaultItemRepository.SaveChangesAsync(cancellationToken);
        }

        protected abstract Task<bool> RemoveAccount(TRequest request, CancellationToken cancellationToken);
        protected abstract Task<bool> ArchiveAccount(TRequest request, CancellationToken cancellationToken);
    }
}