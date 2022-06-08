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
	public class PropertySyncPriceEventHandler : BaseEventConsumer<PropertySyncPriceEvent>
	{
		private readonly IDispatcher _dispatcher;

		public PropertySyncPriceEventHandler(LoggerContext logger, IDispatcher dispatcher) : base(logger)
		{
			_dispatcher = dispatcher;
		}

		public override async Task Consume(ConsumeContext<PropertySyncPriceEvent> messageContext)
		{
			await base.Consume(messageContext);

			var updatePropertyCommand = new UpdateProperty.Command()
			{
				PropertyId = new Guid(messageContext.Message.PropertyId),
				PropertyName = messageContext.Message.PropertyName,
				Address = messageContext.Message.PropertyAddress,
				Postcode = messageContext.Message.Postcode,
				AutomaticallyReValueProperty = messageContext.Message.AutoRevalue,
				PropertyValue = messageContext.Message.NewPropertyValue,
				PropertyValueChange = true,
				NumberOfBedrooms = messageContext.Message.NumberOfBedrooms
			};

			var cancellationTokenSource = new CancellationTokenSource();
			await _dispatcher.DispatchAsync(updatePropertyCommand, cancellationTokenSource.Token);
		}
	}
}
