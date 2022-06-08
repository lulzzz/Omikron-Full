using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.Sync.UkVehicleData.Channels;

namespace Omikron.Sync.UkVehicleData.Extensions
{
	public static class SyncExtensions
	{
		public static IServiceCollection AddVehicleSync(this IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddScoped<ISyncAgent<Vehicle>, UkVehicleDataSyncAgent>()
				.AddSyncChannel()
				.AddVehicleSyncChannelDependencies();

			return serviceCollection;
		}

		public static IServiceCollection AddVehicleSyncChannelDependencies(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<ISyncSource<Vehicle, AssetPrice>, UkVehicleDataValueSyncSource>()
				.AddScoped<ISyncTarget<Vehicle, AssetPrice>, UkVehicleDataValueSyncTarget>();
		}

		public static IServiceCollection AddSyncChannel(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<ISyncChannel<Vehicle>>(implementationFactory: provider =>
					new UkVehicleDataValueSyncChannel(source: provider.GetService<ISyncSource<Vehicle, AssetPrice>>(),
													  target: provider.GetService<ISyncTarget<Vehicle, AssetPrice>>(),
													  loggerContext: provider.GetService<LoggerContext>())
				);
		}
	}
}