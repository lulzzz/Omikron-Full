using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class RemoveInvestmentCommandHandler : RemoveManualAccountBaseHandler<RemoveInvestment.Command>
    {
        private readonly IInvestmentRepository _investmentRepository;

        public RemoveInvestmentCommandHandler(IInvestmentRepository investmentRepository, IVaultItemRepository vaultItemRepository) : base(vaultItemRepository)
        {
            _investmentRepository = investmentRepository;
        }

        protected override async Task<bool> ArchiveAccount(RemoveInvestment.Command request, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.GetInvestment(request.AccountId, cancellationToken);
            if (investment.IsNull())
            {
                return false;
            }

            investment.IsArchived = true;
            _investmentRepository.Update(investment);
                
            return true;
        }

        protected override async Task<bool> RemoveAccount(RemoveInvestment.Command request, CancellationToken cancellationToken)
        {
            return await _investmentRepository.DeleteInvestmentById(request.AccountId, cancellationToken);
        }
    }
}