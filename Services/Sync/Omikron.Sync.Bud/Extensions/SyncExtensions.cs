using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.Sync.Bud.Channels.Accounts;
using Omikron.Sync.Bud.Channels.Refresh;
using Omikron.Sync.Bud.Channels.Transactions;
using Omikron.Sync.Bud.Models;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Extensions
{
	public static class SyncExtensions
	{
		public static IServiceCollection AddBudSync(this IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddScoped<ISyncAgent<User>, BudSyncAgent>()
				.AddSyncChannel()
				.AddAccountsSyncChannelDependencies()
				.AddTransactionsSyncChannelDependencies();

			return serviceCollection;
		}

		public static IServiceCollection AddAccountsSyncChannelDependencies(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<ISyncSource<User, CustomerAccountsData>, BudAccountsSyncSource>()
				.AddScoped<ISyncTarget<User, CustomerAccountsData>, BudAccountsSyncTarget>();
		}

		public static IServiceCollection AddTransactionsSyncChannelDependencies(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<ISyncSource<User, IEnumerable<BudListTransactionsResponse>>, BudTransactionSyncSource>()
				.AddScoped<ISyncTarget<User, IEnumerable<BudListTransactionsResponse>>, BudTransactionSyncTarget>();
		}

		public static IServiceCollection AddSyncChannel(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddScoped<IEnumerable<ISyncChannel<User>>>(implementationFactory: provider => new ISyncChannel<User>[]
				{
					new BudInitiateRefreshChannel(dispatcher: provider.GetService<IDispatcher>(),
												  loggerContext: provider.GetService<LoggerContext>()),

					new BudAccountsSyncChannel(source: provider.GetService<ISyncSource<User, CustomerAccountsData>>(), 
											   target: provider.GetService<ISyncTarget<User, CustomerAccountsData>>(), 
											   loggerContext: provider.GetService<LoggerContext>()),

					new BudTransactionSyncChannel(source: provider.GetService<ISyncSource<User, IEnumerable<BudListTransactionsResponse>>>(), 
												  target: provider.GetService<ISyncTarget<User, IEnumerable<BudListTransactionsResponse>>>(),
												  loggerContext: provider.GetService<LoggerContext>())
				});
		}
	}
}