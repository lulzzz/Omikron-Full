using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemovePersonalItemFinanceCommandHandler : BaseHandlerLight<RemovePersonalItemFinance.Command, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public RemovePersonalItemFinanceCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<bool> Handle(RemovePersonalItemFinance.Command request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.DeletePersonalItemFinance(request.AccountId, cancellationToken);

            if (!result)
            {
                return false;
            }

            return await _accountRepository.SaveChangesAsync(cancellationToken);
        }
    }
}