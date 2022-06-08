using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.Sync.Bud.Commands;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Channels.Refresh
{
    public class BudInitiateRefreshChannel : ISyncChannel<User>
    {
        private readonly IDispatcher _dispatcher;
        private readonly LoggerContext _loggerContext;

        public BudInitiateRefreshChannel(IDispatcher dispatcher, LoggerContext loggerContext)
        {
            _dispatcher = dispatcher;
            _loggerContext = loggerContext;
        }

        public async Task<Result<SyncResult>> Sync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _loggerContext.UsageLogger.Information(message: $"Starting sync of: {nameof(BudInitiateRefreshChannel)}.");

                await _dispatcher.DispatchAsync(command: new InitiateRefresh.Command(userId: user.ExternalId, budCustomerId: user.BudCustomerId, budCustomerSecret: user.BudCustomerSecret), cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                _loggerContext.UsageLogger.Error(message: $"Error occurred during sync of: {nameof(BudInitiateRefreshChannel)}. Exception message: {exception.Message}", data: new
                {
                    InnerException = exception.InnerException?.Message,
                    exception.StackTrace
                });
                return new SyncResult(status: SyncStatus.Error, exception: new SyncException(message: exception.Message, inner: exception));
            }

            _loggerContext.UsageLogger.Information(message: $"Sync of: {nameof(BudInitiateRefreshChannel)} completed successfully.");
            return new SyncResult(status: SyncStatus.Success, exception: SyncException.None);
        }
    }
}