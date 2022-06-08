using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.UkVehicleData.Channels
{
	public class UkVehicleDataValueSyncSource : ISyncSource<Vehicle, AssetPrice>
	{
		private readonly IHttpVaultService _httpVaultService;

		public UkVehicleDataValueSyncSource(IHttpVaultService httpVaultService)
		{
			_httpVaultService = httpVaultService;
		}

		public async Task<Maybe<SyncSourcePayload<AssetPrice>>> FetchAsync(Vehicle entity, CancellationToken cancellationToken)
		{
			var value = await _httpVaultService.GetVehicleValue(entity.Registration, entity.Mileage, cancellationToken);

			var payload = new SyncSourcePayload<AssetPrice>(value.Records);

			return Maybe<SyncSourcePayload<AssetPrice>>.From(payload);
		}
	}
}
