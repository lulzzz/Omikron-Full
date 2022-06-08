using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Events;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.PropertyData.Channels
{
	public class PropertyDataValueSyncTarget : ISyncTarget<Property, AssetPrice>
	{
		private readonly IDispatcher _dispatcher;
		private readonly IAccountService _accountService;

		public PropertyDataValueSyncTarget(IDispatcher dispatcher, IAccountService accountService)
		{
			_dispatcher = dispatcher;
			_accountService = accountService;
		}

		public async Task<Result> SaveAsync(Property entity, SyncTargetPayload<AssetPrice> syncTargetPayload, CancellationToken cancellationToken)
		{
			var currentPropertyValue = _accountService.GetLastValueOrZero(entity);

			if (currentPropertyValue.Truncate() != syncTargetPayload.Value.Value.Truncate())
			{
				var vehicleSyncPriceEvent = new PropertySyncPriceEvent(
					entity.Id.ToString(),
					entity.Name,
					entity.Address,
					entity.Postcode,
					syncTargetPayload.Value.Value,
					entity.AutomaticallyReValueProperty,
					entity.NumberOfBedrooms);

				await _dispatcher.PublishEventAsync(vehicleSyncPriceEvent, cancellationToken);
			}

			return Result.Success();
		}
	}
}
