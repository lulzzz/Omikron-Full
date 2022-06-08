using System.Threading.Tasks;
using MassTransit;
using Omikron.SharedKernel.Infrastructure.Logging.Context;

namespace Omikron.SharedKernel.Infrastructure.Bus
{
    /// <summary>
    ///     A base class that is a consumer of a message. The message is wrapped in an IConsumeContext
    ///     interface to allow access to details surrounding the inbound message, including headers.
    /// </summary>
    /// <typeparam name="TEvent">Represent the event type.</typeparam>
    public abstract class BaseEventConsumer<TEvent> : IConsumer<TEvent> where TEvent : class
    {
        protected readonly LoggerContext Logger;

        protected BaseEventConsumer(LoggerContext logger)
        {
            Logger = logger;
        }

        public virtual Task Consume(ConsumeContext<TEvent> messageContext)
        {
            Logger.CorrelationId ??= messageContext.ConversationId?.ToString() ??
                                     messageContext.CorrelationId?.ToString() ?? messageContext.InitiatorId?.ToString();
            Logger.UsageLogger.Information(message: $"Event accepted: '{typeof(TEvent).Name}' from: '{GetType().FullName}.'");
            return Task.CompletedTask;
        }
    }
}