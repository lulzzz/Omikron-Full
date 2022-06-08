using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.Sync.Bud.Models;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Channels.Accounts
{
    public class BudAccountsSyncChannel : ISyncChannel<User>
    {
        private readonly LoggerContext _loggerContext;

        public BudAccountsSyncChannel(ISyncSource<User, CustomerAccountsData> source, ISyncTarget<User, CustomerAccountsData> target, LoggerContext loggerContext)
        {
            Source = source;
            Target = target;
            _loggerContext = loggerContext;
        }

        public ISyncSource<User, CustomerAccountsData> Source { get; }
        public ISyncTarget<User, CustomerAccountsData> Target { get; }

        public async Task<Result<SyncResult>> Sync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _loggerContext.UsageLogger.Information(message: $"Starting sync of: {nameof(BudAccountsSyncChannel)}.");

                var accounts = await Source.FetchAsync(entity: user, cancellationToken: cancellationToken);
                var targetPayload = new SyncTargetPayload<CustomerAccountsData>(value: accounts.Value.Value);

                await Target.SaveAsync(entity: user, syncTargetPayload: targetPayload, cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                _loggerContext.UsageLogger.Error(message: $"Error occurred during sync of: {nameof(BudAccountsSyncChannel)}. Exception message: {exception.Message}", data: new
                {
                    InnerException = exception.InnerException?.Message,
                    exception.StackTrace
                });
                return new SyncResult(status: SyncStatus.Error, exception: new SyncException(message: exception.Message, inner: exception));
            }

            _loggerContext.UsageLogger.Information(message: $"Sync of: {nameof(BudAccountsSyncChannel)} completed successfully.");
            return new SyncResult(status: SyncStatus.Success, exception: SyncException.None);
        }
    }
}