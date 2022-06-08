using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.Sync.UkVehicleData
{
	public sealed class UkVehicleDataSyncAgent : ISyncAgent<Vehicle>
	{
		public UkVehicleDataSyncAgent(ISyncChannel<Vehicle> syncChannel)
		{
			SyncChannels = syncChannel;
		}

		public ISyncChannel<Vehicle> SyncChannels { get; }

		public async Task DoWorkAsync(Vehicle entity, CancellationToken cancellationToken)
		{
			await SyncChannels.Sync(entity, cancellationToken);
		}
	}
}