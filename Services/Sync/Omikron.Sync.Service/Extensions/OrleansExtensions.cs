using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Messaging.Sync;
using Omikron.SharedKernel.Orleans;
using Omikron.SharedKernel.Orleans.Extensions;
using Omikron.SharedKernel.Utils;
using Omikron.Sync.Model;
using Omikron.Sync.Service.Actor.Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Omikron.Sync.Service.Extensions
{
    public static class OrleansExtensions
    {
        public static IServiceCollection AddActorAsOrleans(this IServiceCollection services)
        {
            return services
                .AddTransient<ISynchronisationGrain<User>, UserSynchronisationGrain>()
                .AddTransient<ISynchronisationGrain<Vehicle>, VehicleSynchronisationGrain>()
                .AddTransient<ISynchronisationGrain<Property>, PropertySynchronisationGrain>()
                .AddOrleansGrainProvider();
        }

        public static IHostBuilder UseActorAsOrleans(this IHostBuilder host)
        {
            return host.UseOrleans(
                configureDelegate: (ctx, siloBuilder) =>
                    ConfigureOrleans(
                        builder: siloBuilder,
                        configuration: ctx.Configuration,
                        isDevelopment: ctx.HostingEnvironment.IsLocal()));
        }

        public static IApplicationBuilder UseGroomedUsers(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(callback: () =>
            {
                var scheduleAt = Clock.GetTime().AddSeconds(value: 30);
                var dispatcher = app.ApplicationServices.GetService<IDispatcher>();
                dispatcher.ScheduleEventAsync(@event: new OrchestrateSyncStartEvent(), scheduleAt: scheduleAt).GetAwaiter().GetResult();
                dispatcher.ScheduleEventAsync(@event: new OrchestrateVehicleValueSyncStartEvent(), scheduleAt: scheduleAt).GetAwaiter().GetResult();
            });

            return app;
        }

        private static void ConfigureOrleans(ISiloBuilder builder, IConfiguration configuration, bool isDevelopment)
        {
            var config = configuration.GetSection(key: "OrleansConfiguration").Get<OrleansConfiguration>();
            var instrumentationKey = configuration.GetValue(key: "Logging:ApplicationInsights:Key", defaultValue: string.Empty);

            ConfigureClustering(builder: builder, isDevelopment: isDevelopment, config: config, instrumentationKey: instrumentationKey);
            ConfigureLifeTimeUserSynchronisationGrain(builder: builder);
            ConfigureLifeTimeVehicleSynchronisationGrain(builder: builder);
            ConfigureLifeTimeEntitySynchronisationGrain<Property>(builder: builder);
            AddGrainStorage(builder: builder);
            UseDashboard(builder: builder, config: config);
        }

        private static void ConfigureClustering(ISiloBuilder builder, bool isDevelopment, OrleansConfiguration config, string instrumentationKey)
        {
            if (isDevelopment)
            {
                UseLocalhostClustering(builder: builder);
            }
            else
            {
                UseKubernetesClustering(builder: builder, config: config, instrumentationKey: instrumentationKey);
            }
        }

        private static void AddGrainStorage(ISiloBuilder builder)
        {
            builder
                .AddMemoryGrainStorageAsDefault()
                .ConfigureApplicationParts(configure: parts => parts.AddFromApplicationBaseDirectory());
        }

        private static void UseDashboard(ISiloBuilder builder, OrleansConfiguration config)
        {
            if (config.UseDashboard)
            {
                builder.UseDashboard();
            }
        }

        private static void UseKubernetesClustering(ISiloBuilder builder, OrleansConfiguration config, string instrumentationKey)
        {
            builder
                .UseAzureStorageClustering(configureOptions: opt => opt.ConnectionString = config.AzureTableStorageConnectionString)
                .UseKubernetesHosting()
                .AddApplicationInsightsTelemetryConsumer(instrumentationKey: instrumentationKey);
        }

        private static void UseLocalhostClustering(ISiloBuilder builder)
        {
            builder.UseLocalhostClustering();
        }

        private static void ConfigureLifeTimeUserSynchronisationGrain(ISiloBuilder builder)
        {
            builder.Configure<GrainCollectionOptions>(configureOptions: options =>
            {
                var fullName = typeof(UserSynchronisationGrain).FullName;
                if (fullName != null)
                {
                    options.ClassSpecificCollectionAge[key: fullName] = TimeSpan.FromDays(value: 7);
                }
            });
        }
        private static void ConfigureLifeTimeVehicleSynchronisationGrain(ISiloBuilder builder)
        {
            builder.Configure<GrainCollectionOptions>(configureOptions: options =>
            {
                var fullName = typeof(VehicleSynchronisationGrain).FullName;
                if (fullName != null)
                {
                    options.ClassSpecificCollectionAge[key: fullName] = TimeSpan.FromDays(value: 7);
                }
            });
        }

        private static void ConfigureLifeTimeEntitySynchronisationGrain<T>(ISiloBuilder builder)
        {
            builder.Configure<GrainCollectionOptions>(configureOptions: options =>
            {
                var fullName = typeof(T).FullName;
                if (fullName != null)
                {
                    options.ClassSpecificCollectionAge[key: fullName] = TimeSpan.FromDays(value: 7);
                }
            });
        }
    }
}