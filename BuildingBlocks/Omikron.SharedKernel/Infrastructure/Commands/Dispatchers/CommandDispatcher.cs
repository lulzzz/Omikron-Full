using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using MassTransit;
using MediatR;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Logging.Context;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public class CommandDispatcher : IDispatcher
    {
        private readonly IBus _bus;
        private readonly IMediator _commandDispatcher;
        private readonly LoggerContext _logger;
        private readonly IMessageScheduler _messageScheduler;
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IMediator commandDispatcher, IServiceProvider serviceProvider, IBus bus, IMessageScheduler messageScheduler, LoggerContext logger)
        {
            _commandDispatcher = commandDispatcher;
            _serviceProvider = serviceProvider;
            _bus = bus;
            _messageScheduler = messageScheduler;
            _logger = logger;
        }

        public virtual Task<TResult> DispatchAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _commandDispatcher.Send(request: command, cancellationToken: cancellationToken);
        }

        public virtual Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default(CancellationToken))
        {
            DomainEvents.Dispatch(domainEvent: domainEvent, serviceProvider: _serviceProvider);
            return Task.CompletedTask;
        }

        public Task PublishEventAsync(object @event, CancellationToken cancellationToken = default)
        {
            _bus.Publish(message: @event, cancellationToken: cancellationToken).SafeFireAndForget(onException: exception => _logger.ErrorLogger.Error(exception: exception));
            return Task.CompletedTask;
        }

        public Task PublishEventAsync(IList<object> events, CancellationToken cancellationToken = default)
        {
            _bus.Publish(message: events, cancellationToken: cancellationToken).SafeFireAndForget(onException: exception => _logger.ErrorLogger.Error(exception: exception));
            return Task.CompletedTask;
        }

        public Task ScheduleEventAsync(object @event, DateTime scheduleAt, CancellationToken cancellationToken = default)
        {
            _messageScheduler.SchedulePublish(scheduledTime: scheduleAt, message: @event, cancellationToken: cancellationToken).SafeFireAndForget(onException: exception => _logger.ErrorLogger.Error(exception: exception));
            return Task.CompletedTask;
        }
    }
}