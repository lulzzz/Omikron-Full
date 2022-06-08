using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Logging.Types;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;

namespace Omikron.SharedKernel.Infrastructure.Logging.Implementations.Serilog
{
    public class SerilogLogger : ILogger, IPerformanceLogger, IUsageLogger, IErrorLogger, IDiagnosticLogger
    {
        public SerilogLogger(IConfiguration configuration)
        {
            var url = configuration.GetValue<string>("Logging:Seq:Url");
            var apiKey = configuration.GetValue<string>("Logging:Seq:ApiKey");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.Seq(url, apiKey: apiKey)
                .CreateLogger();
        }

        public string CorrelationId { get; set; }

        public string ParentId { get; set; }

        public void Debug(string message)
        {
            AddMetaData();
            Log.Debug(message);
        }

        public void Debug<TData>(string message, TData data)
        {
            AddMetaData();
            Log.Logger.Data(data).Debug(message);
        }

        public void Error(Exception exception)
        {
            AddMetaData();
            Log.Error(exception, exception.Message);
        }

        public void Error<TData>(Exception exception, TData data)
        {
            AddMetaData();
            Log.Logger.Data(data).Error(exception, exception.Message);
        }

        public void Error(string message)
        {
            AddMetaData();
            Log.Error(message);
        }

        public void Error<TData>(string message, TData data)
        {
            AddMetaData();
            Log.Logger.Data(data).Error(message);
        }

        public void Fatal(Exception exception)
        {
            AddMetaData();
            Log.Fatal(exception, exception.Message);
        }

        public void Fatal<TData>(Exception exception, TData data)
        {
            AddMetaData();
            Log.Logger.Data(data).Fatal(exception, exception.Message);
        }

        public void Information(string message)
        {
            AddMetaData();
            Log.Information(message);
        }

        public void Information<TData>(string message, TData data)
        {
            AddMetaData();
            Log.Logger.Data(data).Information(message);
        }

        public void Warning(string message)
        {
            AddMetaData();
            Log.Warning(message);
        }

        public void Warning<TData>(string message, TData data)
        {
            AddMetaData();
            Log.Logger.Data(data).Warning(message);
        }

        private void AddMetaData()
        {
            if (!string.IsNullOrWhiteSpace(CorrelationId))
                using (LogContext.PushProperty(LoggerConstants.CorrelationId, CorrelationId)) ;
        }

        public TResult TrackDependency<TResult>(Func<TResult> operation, string dependencyType, string dependencyName, string dependencyCommand)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                TResult value = operation();
                stopWatch.Stop();
                TrackDependency(dependencyType, dependencyName, dependencyCommand, true, stopWatch.Elapsed);
                return value;
            }
            catch
            {
                stopWatch.Stop();
                TrackDependency(dependencyType, dependencyName, dependencyCommand, false, stopWatch.Elapsed);
                throw;
            }
        }

        public void TrackDependency(Action operation, string dependencyType, string dependencyName, string dependencyCommand)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                operation();
                stopWatch.Stop();
                TrackDependency(dependencyType, dependencyName, dependencyCommand, true, stopWatch.Elapsed);
            }
            catch
            {
                stopWatch.Stop();
                TrackDependency(dependencyType, dependencyName, dependencyCommand, false, stopWatch.Elapsed);
                throw;
            }
        }

        private void TrackDependency(string dependencyType, string dependencyName, string dependencyCommand, bool success, TimeSpan duration)
        {
            Debug($"Dependency: {dependencyType}/{dependencyName} Command: {dependencyCommand} Success: {success} TimeTaken: {duration.ToString()}");
        }

        public TResult TrackOperation<TResult>(Func<TResult> operation, string name)
        {

            var stopWatch = Stopwatch.StartNew();
            try
            {
                TResult value = operation();
                stopWatch.Stop();
                Debug($"{name} Success: {true} TimeTaken: {stopWatch.Elapsed}");
                return value;
            }
            catch
            {
                stopWatch.Stop();
                Debug($"{name} Success: {false} TimeTaken: {stopWatch.Elapsed}");
                throw;
            }
        }

        public IOperationHolder<T> StartOperation<T>(T operationTelemetry) where T : OperationTelemetry
        {
            Debug($"Starting operation {operationTelemetry.Name}");
            return default(IOperationHolder<T>);
        }

        public void StopOperation<T>(IOperationHolder<T> operation, string operationName = "") where T : OperationTelemetry
        {
            Debug($"Execution of {operationName} has ended");
        }

        public async Task<TResult> TrackOperation<TResult>(Func<Task<TResult>> operation, string name, IDictionary<string, string> customProperties = null)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                TResult value = await operation();
                stopWatch.Stop();
                Debug($"{name} Success: {true} TimeTaken: {stopWatch.Elapsed}", customProperties);
                return value;
            }
            catch
            {
                stopWatch.Stop();
                Debug($"{name} Success: {false} TimeTaken: {stopWatch.Elapsed}", customProperties);
                throw;
            }
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            var propertiesDump = (properties != null) ? string.Join(",", properties.Select(d => $"{d.Key}:{d.Value}")) : "";
            
            var metricsDump = (metrics != null) ? string.Join(",", metrics.Select(d => $"{d.Key}:{d.Value}")) : "";

            Debug($"{eventName}, Properties: {propertiesDump}, Metrics: {metricsDump}");
        }

        public void TrackMetric(string eventName, double value, IDictionary<string, string> properties = null)
        {
            var propertiesDump = (properties != null) ? string.Join(",", properties.Select(d => $"{d.Key}:{d.Value}")) : "";

            Debug($"{eventName}, Metric: {value}, Properties: {propertiesDump}");
        }
    }
}