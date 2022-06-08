using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemovePersonalItemCommandHandler : RemoveManualAccountBaseHandler<RemovePersonalItem.Command>
    {
        private readonly IPersonalItemRepository _personalItemRepository;

        public RemovePersonalItemCommandHandler(IPersonalItemRepository personalItemRepository, IVaultItemRepository vaultItemRepository) : base(vaultItemRepository)
        {
            _personalItemRepository = personalItemRepository;
        }

        protected override async Task<bool> ArchiveAccount(RemovePersonalItem.Command request, CancellationToken cancellationToken)
        {
            var personalItem = await _personalItemRepository.GetPersonalItem(request.AccountId, cancellationToken);
            if (personalItem.IsNull())
            {
                return false;
            }

            personalItem.IsArchived = true;
            await _personalItemRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override async Task<bool> RemoveAccount(RemovePersonalItem.Command request, CancellationToken cancellationToken)
        {
            return await _personalItemRepository.RemovePersonalItemById(request.AccountId, cancellationToken);
        }
    }
}