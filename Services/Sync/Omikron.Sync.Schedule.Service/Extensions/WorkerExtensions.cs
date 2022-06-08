using System;
using System.Configuration;
using System.Linq;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Utils;
using Omikron.Sync.Schedule.Service.Workers;

namespace Omikron.Sync.Schedule.Service.Extensions
{
	public static class WorkerExtensions
	{
		public static IApplicationBuilder UseOrchestrateSyncWorker(this IApplicationBuilder app)
		{
			var syncConfiguration = LoadSyncConfiguration(app: app);

			app.ApplicationServices.UseScheduler(assignScheduledTasks: scheduler =>
			{
				foreach (var onTime in syncConfiguration.Interval.Recurrence)
				{
					scheduler
						.OnWorker(workerName: $"{nameof(OrchestrateSyncStartWorker)}-{onTime.Hours}-{onTime.Minutes}-UTC")
						.Schedule<OrchestrateSyncStartWorker>()
						.DailyAt(hour: onTime.Hours, minute: onTime.Minutes)
						.Zoned(timeZoneInfo: TimeZoneInfo.Utc)
						.PreventOverlapping(uniqueIdentifier: nameof(OrchestrateSyncStartWorker));
				}
			});

			return app;
		}

		public static IApplicationBuilder UseOrchestrateVehicleSyncWorker(this IApplicationBuilder app)
		{
			var cron = LoadSyncConfigurationCronValue(app, "SyncConfiguration:VehicleSyncInterval");

			app.ApplicationServices.UseScheduler(assignScheduledTasks: scheduler =>
			{
				scheduler
					.OnWorker(workerName: $"{nameof(OrchestrateVehicleSyncStartWorker)}-{Clock.GetTime().Month}")
					.Schedule<OrchestrateVehicleSyncStartWorker>()
					.Cron(cron)
					.Zoned(timeZoneInfo: TimeZoneInfo.Utc)
					.PreventOverlapping(uniqueIdentifier: nameof(OrchestrateVehicleSyncStartWorker));
			});

			return app;
		}

		public static IApplicationBuilder UseOrchestratePropertySyncWorker(this IApplicationBuilder app)
		{
			var cron = LoadSyncConfigurationCronValue(app, "SyncConfiguration:PropertySyncInterval");

			app.ApplicationServices.UseScheduler(assignScheduledTasks: scheduler =>
			{
				scheduler
					.OnWorker(workerName: $"{nameof(OrchestratePropertySyncStartWorker)}-{Clock.GetTime().Month}")
					.Schedule<OrchestratePropertySyncStartWorker>()
					.Cron(cron)
					.Zoned(timeZoneInfo: TimeZoneInfo.Utc)
					.PreventOverlapping(uniqueIdentifier: nameof(OrchestratePropertySyncStartWorker));
			});

			return app;
		}

		private static SyncConfiguration LoadSyncConfiguration(IApplicationBuilder app)
		{
			var intervals = app.ApplicationServices.GetService<IConfiguration>()?.GetSection(key: "SyncConfiguration:SyncInterval").Get<string[]>();

			if (intervals == null || !intervals.Any())
			{
				throw new ConfigurationErrorsException(message: "The section Sync Configuration is missing or the intervals are missing.");
			}

			return new SyncConfiguration(interval: SyncInterval.Parse(values: intervals));
		}

		private static string LoadSyncConfigurationCronValue(IApplicationBuilder app, string configurationKey)
		{
			var cron = app.ApplicationServices.GetService<IConfiguration>()?.GetSection(key: configurationKey).Get<string>();

			if (cron.IsNullOrEmpty())
			{
				throw new ConfigurationErrorsException(message: $"The section {configurationKey} is missing.");
			}

			return cron;
		}
	}
}