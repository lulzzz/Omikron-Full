using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.Sync.PropertyData.Channels;

namespace Omikron.Sync.PropertyData.Extensions
{
	public static class SyncExtensions
	{
		public static IServiceCollection AddPropertySync(this IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddScoped<ISyncAgent<Property>, PropertyDataSyncAgent>()
				.AddSyncChannel()
				.AddPropertySyncChannelDependencies();

			return serviceCollection;
		}

		public static IServiceCollection AddPropertySyncChannelDependencies(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<ISyncSource<Property, AssetPrice>, PropertyDataValueSyncSource>()
				.AddScoped<ISyncTarget<Property, AssetPrice>, PropertyDataValueSyncTarget>();
		}

		public static IServiceCollection AddSyncChannel(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<ISyncChannel<Property>>(implementationFactory: provider =>
					new PropertyDataValueSyncChannel(source: provider.GetService<ISyncSource<Property, AssetPrice>>(),
													 target: provider.GetService<ISyncTarget<Property, AssetPrice>>(),
													 loggerContext: provider.GetService<LoggerContext>())
				);
		}
	}
}