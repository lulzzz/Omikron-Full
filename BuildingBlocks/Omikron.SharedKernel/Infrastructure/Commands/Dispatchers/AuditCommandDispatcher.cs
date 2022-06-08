using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Logging.Context;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public sealed class AuditCommandDispatcher : CommandDispatcher
    {
        private readonly LoggerContext _logger;

        public AuditCommandDispatcher(IMediator commandDispatcher, IServiceProvider serviceProvider, IMessageScheduler messageScheduler, IBus bus, LoggerContext logger)
            : base(commandDispatcher: commandDispatcher, serviceProvider: serviceProvider, bus: bus, messageScheduler: messageScheduler, logger: logger)
        {
            _logger = logger;
        }

        public override Task<TResult> DispatchAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _logger.PerformanceLogger.TrackOperation(operation: () => base.DispatchAsync(command: command, cancellationToken: cancellationToken), name: $"Handling command {command.GetType().FullName}", customProperties: command.GetLoggerProperties());
        }

        public override async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.DispatchAsync(domainEvent: domainEvent, cancellationToken: cancellationToken);
        }
    }
}