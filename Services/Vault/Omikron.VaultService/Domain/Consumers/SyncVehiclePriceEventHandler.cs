using MassTransit;
using Omikron.SharedKernel.Infrastructure.Bus;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Vault.Events;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Consumers
{
	public class SyncVehiclePriceEventHandler : BaseEventConsumer<VehicleSyncPriceEvent>
	{
		private readonly IDispatcher _dispatcher;

		public SyncVehiclePriceEventHandler(LoggerContext logger, IDispatcher dispatcher) : base(logger)
		{
			_dispatcher = dispatcher;
		}

		public override async Task Consume(ConsumeContext<VehicleSyncPriceEvent> messageContext)
		{
			await base.Consume(messageContext);

			var updateVehicleCommand = new UpdateVehicle.Command()
			{
				VehicleId = new Guid(messageContext.Message.VehicleId),
				Registration = messageContext.Message.VehicleRegistration,
				VehicleName = messageContext.Message.VehicleName,
				Mileage = messageContext.Message.VehicleMileage,
				AutomaticallyReValueVehicle = messageContext.Message.AutoRevalue,
				VehicleValue = messageContext.Message.NewVehicleValue,
				VehicleValueChanged = true
			};

			var cancellationTokenSource = new CancellationTokenSource();
			await _dispatcher.DispatchAsync(updateVehicleCommand, cancellationTokenSource.Token);
		}
	}
}
