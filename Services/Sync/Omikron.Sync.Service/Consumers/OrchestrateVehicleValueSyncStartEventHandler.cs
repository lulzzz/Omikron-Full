using System.Threading.Tasks;
using MassTransit;
using Omikron.SharedKernel.Infrastructure.Bus;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Messaging.Sync;
using Omikron.Sync.Service.Business.Commands;

namespace Omikron.Sync.Service.Consumers
{
	public class OrchestrateVehicleValueSyncStartEventHandler : BaseEventConsumer<OrchestrateVehicleValueSyncStartEvent>
	{
		private readonly IDispatcher _dispatcher;

		public OrchestrateVehicleValueSyncStartEventHandler(LoggerContext logger, IDispatcher dispatcher) : base(logger)
		{
			_dispatcher = dispatcher;
		}

		public override async Task Consume(ConsumeContext<OrchestrateVehicleValueSyncStartEvent> messageContext)
		{
			await base.Consume(messageContext);
			await _dispatcher.DispatchAsync(new OrchestrateVehicleValueSync.Command());
		}
	}
}
