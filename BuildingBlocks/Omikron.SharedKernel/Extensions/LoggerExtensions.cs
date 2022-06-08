using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omikron.SharedKernel.Infrastructure.Configuration;
using Omikron.SharedKernel.Infrastructure.Logging;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Logging.Implementations;
using Omikron.SharedKernel.Infrastructure.Logging.Implementations.ApplicationInsights;
using Omikron.SharedKernel.Infrastructure.Logging.Implementations.Serilog;
using Omikron.SharedKernel.Infrastructure.Logging.Types;
using ILogger = Serilog.ILogger;

namespace Omikron.SharedKernel.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger Data(this ILogger log, object data)
        {
            return log.ForContext("@Data", data, true);
        }

        public static IServiceCollection AddLogger(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var loggerType = configuration.GetValue<SupportedLoggers>("Logging:Use");

            switch (loggerType)
            {
                case SupportedLoggers.Serilog:
                    serviceCollection.AddSerilog();
                    break;
                case SupportedLoggers.ApplicationInsight:
                    serviceCollection.AddApplicationInsight(configuration);
                    break;
                default:
                    serviceCollection.AddNullLogger();
                    break;
            }

            serviceCollection.AddSingleton<LoggerContext, LoggerContext>();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Information));
            return serviceCollection;
        }

        public static IServiceCollection AddSerilog(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPerformanceLogger, SerilogLogger>();
            serviceCollection.AddSingleton<IUsageLogger, SerilogLogger>();
            serviceCollection.AddSingleton<IErrorLogger, SerilogLogger>();
            serviceCollection.AddSingleton<IDiagnosticLogger, SerilogLogger>();
            return serviceCollection;
        }

        public static IServiceCollection AddApplicationInsight(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<ApplicationInsightsConfiguration>(configuration
                .GetSection(Configurations.LOGGING_APPLICATIONINSIGHTS));

            var applicationInsightsConfiguration = configuration
                .GetSection(Configurations.LOGGING_APPLICATIONINSIGHTS)
                .Get<ApplicationInsightsConfiguration>();

            serviceCollection.AddSingleton<IPerformanceLogger, ApplicationInsightLogger>();
            serviceCollection.AddSingleton<IUsageLogger, ApplicationInsightLogger>();
            serviceCollection.AddSingleton<IErrorLogger, ApplicationInsightLogger>();
            serviceCollection.AddSingleton<IDiagnosticLogger, ApplicationInsightLogger>();

            serviceCollection.AddApplicationInsightsTelemetry(options =>
            {
                options.InstrumentationKey = applicationInsightsConfiguration.Key;
                options.EnableDependencyTrackingTelemetryModule = applicationInsightsConfiguration.EnableDependencyTrackingTelemetryModule;
                options.EnableRequestTrackingTelemetryModule = applicationInsightsConfiguration.EnableRequestTrackingTelemetryModule;
                options.EnableAdaptiveSampling = applicationInsightsConfiguration.EnableSampling;
            });

            serviceCollection.AddSingleton<ITelemetryInitializer, AppInsightsTelemetryInitialiser>();
            serviceCollection.AddApplicationInsightsTelemetryProcessor<ServiceBusTelemetryProcessor>();

            return serviceCollection;
        }

        public static IServiceCollection AddNullLogger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LoggerContext, LoggerContext>();
            serviceCollection.AddSingleton<IPerformanceLogger, NullLogger>();
            serviceCollection.AddSingleton<IUsageLogger, NullLogger>();
            serviceCollection.AddSingleton<IErrorLogger, NullLogger>();
            serviceCollection.AddSingleton<IDiagnosticLogger, NullLogger>();
            return serviceCollection;
        }
    }
}