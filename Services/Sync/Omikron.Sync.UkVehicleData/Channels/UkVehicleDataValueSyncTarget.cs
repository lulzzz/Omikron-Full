using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Events;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.UkVehicleData.Channels
{
	public class UkVehicleDataValueSyncTarget : ISyncTarget<Vehicle, AssetPrice>
	{
		private readonly IDispatcher _dispatcher;
		private readonly IAccountService _accountService;

		public UkVehicleDataValueSyncTarget(IDispatcher dispatcher, IAccountService accountService)
		{
			_dispatcher = dispatcher;
			_accountService = accountService;
		}

		public async Task<Result> SaveAsync(Vehicle entity, SyncTargetPayload<AssetPrice> syncTargetPayload, CancellationToken cancellationToken)
		{
			var currentVehicleValue = _accountService.GetLastValueOrZero(entity);

			if (currentVehicleValue.Truncate() != syncTargetPayload.Value.Value.Truncate())
			{
				var vehicleSyncPriceEvent = new VehicleSyncPriceEvent(
					entity.Id.ToString(),
					entity.Registration,
					entity.Name,
					entity.Mileage.ToInt32(),
					entity.AutomaticallyReValueVehicle,
					syncTargetPayload.Value.Value);

				await _dispatcher.PublishEventAsync(vehicleSyncPriceEvent, cancellationToken);
			}

			return Result.Success();
		}
	}
}
