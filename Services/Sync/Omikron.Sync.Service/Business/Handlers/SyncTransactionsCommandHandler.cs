using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Accumulators;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.Sync.Bud.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Service.Business.Handlers
{
	public class SyncTransactionsCommandHandler : BaseHandlerLight<SyncTransactions.Command, ApiResult>
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly IMerchantRepository _merchantRepository;

		public SyncTransactionsCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMerchantRepository merchantRepository)
		{
			_transactionRepository = transactionRepository;
			_accountRepository = accountRepository;
			_merchantRepository = merchantRepository;
		}

		public override async Task<ApiResult> Handle(SyncTransactions.Command request, CancellationToken cancellationToken)
		{
			var existingUserTransactions = await _transactionRepository.GetAllUserTransactions(CustomerId.Parse(request.UserId), cancellationToken);
			var existingMerchants = await _merchantRepository.GetAll(cancellationToken);
			var userAccounts = await _accountRepository.GetAccountsByOwnerId(CustomerId.Parse(request.UserId), cancellationToken);

			var dataToAddOrUpdate = request.BudTransactions.Aggregate(new TransactionsAccumulator(existingUserTransactions, existingMerchants, userAccounts), (acc, t) => acc.Accumulate(t), acc => acc);

			await _merchantRepository.AddRangeAsync(dataToAddOrUpdate.MerchantsToAdd, cancellationToken);
			await _transactionRepository.AddRangeAsync(dataToAddOrUpdate.TransactionsToAdd, cancellationToken);
			_transactionRepository.UpdateRange(dataToAddOrUpdate.TransactionsToUpdate);
			await _transactionRepository.SaveAsync(cancellationToken);

			return ApiResult.Success();
		}
	}
}
