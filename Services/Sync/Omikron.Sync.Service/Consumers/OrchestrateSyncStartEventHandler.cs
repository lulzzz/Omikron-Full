using System.Threading.Tasks;
using MassTransit;
using Omikron.SharedKernel.Infrastructure.Bus;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Messaging.Sync;
using Omikron.Sync.Service.Business.Commands;

namespace Omikron.Sync.Service.Consumers
{
    public sealed class OrchestrateSyncStartEventHandler : BaseEventConsumer<OrchestrateSyncStartEvent>
    {
        private readonly IDispatcher _dispatcher;

        public OrchestrateSyncStartEventHandler(LoggerContext loggerContext, IDispatcher dispatcher) : base(logger: loggerContext)
        {
            _dispatcher = dispatcher;
        }

        public override async Task Consume(ConsumeContext<OrchestrateSyncStartEvent> messageContext)
        {
            await base.Consume(messageContext: messageContext);
            await _dispatcher.DispatchAsync(command: new OrchestrateSync.Command());
        }
    }
}