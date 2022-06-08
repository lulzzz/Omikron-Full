using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class UpdateManualAccountCommandHandler : UpdateManualAccountBase<UpdateManualAccount.Command>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateManualAccountCommandHandler(IMapper mapper, IDispatcher dispatcher, IVaultItemRepository vaultItemRepository, ITransactionRepository transactionRepository, IAccountRepository accountRepository, IAccountBalanceRepository accountBalanceRepository) : base(mapper, dispatcher, vaultItemRepository, transactionRepository, accountBalanceRepository)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<ApiResult> Handle(UpdateManualAccount.Command request, CancellationToken cancellationToken)
        {
            var manualAccount = await _accountRepository.GetAccount(request.AccountId, cancellationToken);
            if (manualAccount == null)
            {
                return ApiResult.BadRequest();
            }

            var accountExits = await _accountRepository.AccountExists(manualAccount.OwnerId, request.Name, cancellationToken, manualAccount.Id);
            if (accountExits)
            {
                return ApiResult.BadRequest($"Account with name {request.Name} already exists. Please try a different name.");
            }

            Mapper.Map(request, manualAccount);

            if (request.AccountBalanceChanged)
            {
                await FactoryCreateAccountTransaction(manualAccount, request.Balance, cancellationToken);
            }

            if (!await UpdateVaultItem(manualAccount.Id, request.Balance, request.Name, null, cancellationToken)) 
            {
                return ApiResult.BadRequest();
            }

            _accountRepository.Update(manualAccount);

            return await _accountRepository.SaveChangesAsync(cancellationToken)
                ? ApiResult.Success()
                : ApiResult.BadRequest();
        }
    }
}