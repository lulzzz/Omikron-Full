using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Omikron.SharedKernel.Infrastructure.DataTrace;
using Omikron.SharedKernel.Infrastructure.DataTrace.Query;
using Omikron.SharedKernel.Infrastructure.Jobs;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public sealed class DataTraceCommandDispatcher : CommandDispatcher
    {
        private readonly IDataTraceManager<AzureTableStorageDataTraceQuery> _dataTraceManager;
        private readonly LoggerContext _loggerContext;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IPrincipal _user;

        public DataTraceCommandDispatcher(
            LoggerContext loggerContext,
            IBackgroundTaskQueue queue,
            IHttpContextAccessor httpContextAccessor,
            IDataTraceManager<AzureTableStorageDataTraceQuery> dataTraceManager,
            IMediator commandDispatcher,
            IMessageScheduler messageScheduler,
            IServiceProvider serviceProvider,
            IBus sendBus) : base(commandDispatcher: commandDispatcher, serviceProvider: serviceProvider, bus: sendBus, messageScheduler: messageScheduler, logger: loggerContext)
        {
            _loggerContext = loggerContext;
            _queue = queue;
            _user = httpContextAccessor?.HttpContext?.User;
            _dataTraceManager = dataTraceManager;
        }

        public override async Task<TResult> DispatchAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await base.DispatchAsync(command: command, cancellationToken: cancellationToken);
            TryToSaveTraceData(command: command);
            return response;
        }

        private void TryToSaveTraceData(IBaseRequest command)
        {
            try
            {
                var dataTraceAllowed = CheckIfDataTraceAllowed(command: command);
                if (dataTraceAllowed)
                {
                    _queue.QueueBackgroundWorkItem(workItem: token => SaveDataChangeLogAsync(command: command));
                }
            }
            catch (Exception e)
            {
                _loggerContext.ErrorLogger.Error(message: $"The data trace cannot be saved due error: {e.Message}", data: e);
            }
        }

        private async Task SaveDataChangeLogAsync(IBaseRequest command)
        {
            try
            {
                var type = command.GetType();
                var now = Clock.GetTime();
                var commandName = DataChangeLogCommandName.Parse(commandName: type.FullName);
                var partitionKey = DataChangeLogPartitionKey.Parse(username: _user.Identity.Name);
                var rowKey = DataChangeLogRowKey.Parse(executedAtUtc: now);
                var domain = DataChangeLogDomain.Parse(commandType: type);
                var payload = DataChangeLogJsonPayload.Parse(@object: command);

                var record = new DataChangeLog(
                    logPartitionKey: partitionKey,
                    logRowKey: rowKey,
                    commandName: commandName,
                    domain: domain,
                    payload: payload,
                    executedAtUtc: now);

                await _dataTraceManager.SaveAsync(dataChangeLogs: new List<DataChangeLog> { record });
            }
            catch (Exception e)
            {
                _loggerContext.ErrorLogger.Error(message: $"The data trace cannot be saved due error: {e.Message}", data: e);
            }
        }

        private bool CheckIfDataTraceAllowed(IBaseRequest command)
        {
            return _user != null && _user.Identity.IsAuthenticated && command.GetType().Name
                .Equals(value: "command", comparisonType: StringComparison.CurrentCultureIgnoreCase);
        }
    }
}