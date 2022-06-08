using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemoveAccountCommandHandler : RemoveManualAccountBaseHandler<RemoveAccount.Command>
    {
        private readonly IAccountRepository _accountRepository;

        public RemoveAccountCommandHandler(IAccountRepository accountRepository, IVaultItemRepository vaultItemRepository) : base(vaultItemRepository)
        {
            _accountRepository = accountRepository;
        }

        protected override async Task<bool> ArchiveAccount(RemoveAccount.Command request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccount(request.AccountId, cancellationToken);
            if(account.IsNull())
            {
                return false;
            }

            account.IsArchived = true;
            await _accountRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override async Task<bool> RemoveAccount(RemoveAccount.Command request, CancellationToken cancellationToken)
        {
            return await _accountRepository.DeleteAccountByExternalId(request.AccountId, cancellationToken);
        }
    }
}