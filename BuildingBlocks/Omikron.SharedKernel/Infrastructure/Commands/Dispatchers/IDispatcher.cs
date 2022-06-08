using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public interface IDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default(CancellationToken));
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default(CancellationToken));
        Task PublishEventAsync(object @event, CancellationToken cancellationToken = default);
        Task PublishEventAsync(IList<object> events, CancellationToken cancellationToken = default);
        Task ScheduleEventAsync(object @event, DateTime scheduleAt, CancellationToken cancellationToken = default);
    }
}