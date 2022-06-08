using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.Sync.Bud.Commands;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Channels.Transactions
{
	public sealed class BudTransactionSyncTarget : ISyncTarget<User, IEnumerable<BudListTransactionsResponse>>
    {
        private readonly IDispatcher _dispatcher;

        public BudTransactionSyncTarget(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<Result> SaveAsync(User entity, SyncTargetPayload<IEnumerable<BudListTransactionsResponse>> syncTargetPayload, CancellationToken cancellationToken)
        {
            await _dispatcher.DispatchAsync(command: new SyncTransactions.Command(userId: entity.ExternalId, budTransactions: syncTargetPayload.Value), cancellationToken: cancellationToken);
            return Result.Success();
        }
    }
}