using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Channels.Transactions
{
    public sealed class BudTransactionSyncChannel : ISyncChannel<User>
    {
        private readonly LoggerContext _loggerContext;

        public BudTransactionSyncChannel(ISyncSource<User, IEnumerable<BudListTransactionsResponse>> source, ISyncTarget<User, IEnumerable<BudListTransactionsResponse>> target, LoggerContext loggerContext)
        {
            Source = source;
            Target = target;
            _loggerContext = loggerContext;
        }

        public ISyncSource<User, IEnumerable<BudListTransactionsResponse>> Source { get; set; }
        public ISyncTarget<User, IEnumerable<BudListTransactionsResponse>> Target { get; set; }

        public async Task<Result<SyncResult>> Sync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _loggerContext.UsageLogger.Information(message: $"Starting sync of: {nameof(BudTransactionSyncChannel)}.");

                var transactions = await Source.FetchAsync(entity: user, cancellationToken: cancellationToken);
                var targetPayload = new SyncTargetPayload<IEnumerable<BudListTransactionsResponse>>(value: transactions.Value.Value);

                await Target.SaveAsync(entity: user, syncTargetPayload: targetPayload, cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                _loggerContext.UsageLogger.Error(message: $"Error occurred during sync of: {nameof(BudTransactionSyncChannel)}. Exception message: {exception.Message}", data: new
                {
                    InnerException = exception.InnerException?.Message, exception.StackTrace
                });
                return new SyncResult(status: SyncStatus.Error, exception: new SyncException(message: exception.Message, inner: exception));
            }

            _loggerContext.UsageLogger.Information(message: $"Sync of: {nameof(BudTransactionSyncChannel)} completed successfully.");
            return new SyncResult(status: SyncStatus.Success, exception: SyncException.None);
        }
    }
}