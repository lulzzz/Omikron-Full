using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Types;
using Omikron.SharedKernel.Infrastructure.Serialization;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Configuration;

namespace Omikron.SharedKernel.Infrastructure.Logging.Implementations.ApplicationInsights
{
    public class ApplicationInsightLogger : ILogger, IPerformanceLogger, IUsageLogger, IErrorLogger,
        IDiagnosticLogger
    {
        private readonly TelemetryClient _telemetryClient;

        private readonly IJsonSerialization _jsonSerialization;

        public string CorrelationId { get; set; }

        public string ParentId { get; set; }

        public ApplicationInsightLogger(IConfiguration configuration, TelemetryClient telemetryClient, IJsonSerialization jsonSerialization)
        {
            _telemetryClient = telemetryClient;
            this._jsonSerialization = jsonSerialization;
        }

        public void Debug(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Verbose);
        }

        public void Debug<TData>(string message, TData data)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Verbose, 
                new Dictionary<string, string> { 
                    { nameof(data), _jsonSerialization.Serialize(data, typeof(TData)) }
                });
        }

        public void Error(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Error);
        }

        public void Error<TData>(string message, TData data)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Error,
                new Dictionary<string, string> { { nameof(data), _jsonSerialization.Serialize(data, typeof(TData)) } });
        }

        public void Error(Exception exception)
        {
            _telemetryClient.TrackException(exception);
        }

        public void Error<TData>(Exception exception, TData data)
        {
            _telemetryClient.TrackException(exception,
                new Dictionary<string, string> { { nameof(data), _jsonSerialization.Serialize(data, typeof(TData)) } });
        }

        public void Fatal(Exception exception)
        {
            _telemetryClient.TrackException(new ExceptionTelemetry
            {
                Exception = exception,
                SeverityLevel = SeverityLevel.Critical
            });
        }

        public void Fatal<TData>(Exception exception, TData data)
        {
            var exceptionTelemetry = new ExceptionTelemetry
            {
                Exception = exception,
                SeverityLevel = SeverityLevel.Critical
            };
            exceptionTelemetry.Properties[nameof(data)] = _jsonSerialization.Serialize(data, typeof(TData));
            _telemetryClient.TrackException(exceptionTelemetry);
        }

        public void Information(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Information);
        }

        public void Information<TData>(string message, TData data)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Information,
                new Dictionary<string, string> {
                    {nameof(data), _jsonSerialization.Serialize(data, typeof(TData))}
                });
        }

        public void Warning(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Warning);
        }

        public void Warning<TData>(string message, TData data)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Warning, new Dictionary<string, string>
                {{nameof(data), _jsonSerialization.Serialize(data, typeof(TData))}});
        }

        public TResult TrackDependency<TResult>(Func<TResult> operation, string dependencyType, string dependencyName, string dependencyCommand)
        {
            var dependencyTelemetry = new DependencyTelemetry
            {
                Type = dependencyType,
                Name = dependencyName,
                Data = dependencyCommand,
            };

            if (!string.IsNullOrWhiteSpace(CorrelationId) && !string.IsNullOrWhiteSpace(ParentId))
            {
                dependencyTelemetry.Context.Operation.Id = CorrelationId;
                dependencyTelemetry.Context.Operation.ParentId = ParentId;
            }

            var dependencyOperation = _telemetryClient.StartOperation(dependencyTelemetry);
            try
            {
                var value = operation();
                _telemetryClient.StopOperation(dependencyOperation);

                return value;
            }
            catch (Exception)
            {
                dependencyOperation.Telemetry.Success = false;
                _telemetryClient.StopOperation(dependencyOperation);
                throw;
            }
        }

        /// <summary>
        ///     Execute the action and log it as a dependency in appsinsights
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="dependencyType"></param>
        /// <param name="dependencyName"></param>
        /// <param name="dependencyCommand"></param>
        public void TrackDependency(Action operation, string dependencyType, string dependencyName, string dependencyCommand)
        {
            var dependencyTelemetry = new DependencyTelemetry
            {
                Type = dependencyType,
                Name = dependencyName,
                Data = dependencyCommand,
            };

            if (!string.IsNullOrWhiteSpace(CorrelationId) && !string.IsNullOrWhiteSpace(ParentId))
            {
                dependencyTelemetry.Context.Operation.Id = CorrelationId;
                dependencyTelemetry.Context.Operation.ParentId = ParentId;
            }

            var dependencyOperation = _telemetryClient.StartOperation(dependencyTelemetry);

            try
            {
                operation();
                _telemetryClient.StopOperation(dependencyOperation);
            }
            catch (Exception)
            {
                dependencyOperation.Telemetry.Success = false;
                _telemetryClient.StopOperation(dependencyOperation);
                throw;
            }
        }

        public async Task<TResult> TrackOperation<TResult>(Func<Task<TResult>> operation, string name, IDictionary<string, string> customProperties = default(Dictionary<string, string>))
        {
            var requestTelemetry = new RequestTelemetry { Name = name };
            customProperties.ForEach(customProperty => requestTelemetry.Properties.Add(customProperty));

            if (!string.IsNullOrWhiteSpace(CorrelationId) && !string.IsNullOrWhiteSpace(ParentId))
            {
                requestTelemetry.Context.Operation.Id = CorrelationId;
                requestTelemetry.Context.Operation.ParentId = ParentId;
            }

            var requestOperation = _telemetryClient.StartOperation(requestTelemetry);

            try
            {
                var value = await operation();
                _telemetryClient.StopOperation(requestOperation);

                CorrelationId = requestOperation.Telemetry.Context.Operation.Id;
                ParentId = requestOperation.Telemetry.Id;

                return value;
            }

            catch (Exception e)
            {
                requestTelemetry.Success = false;
                _telemetryClient.StopOperation(requestOperation);
                CorrelationId = requestOperation.Telemetry.Context.Operation.Id;
                ParentId = requestOperation.Telemetry.Id;
                throw;
            }
        }

        public IOperationHolder<T> StartOperation<T>(T operationTelemetry) where T : OperationTelemetry
        {
            if (!string.IsNullOrWhiteSpace(CorrelationId) && !string.IsNullOrWhiteSpace(ParentId))
            {
                operationTelemetry.Context.Operation.Id = CorrelationId;
                operationTelemetry.Context.Operation.ParentId = ParentId;
            }
            var operation = _telemetryClient.StartOperation(operationTelemetry);
            CorrelationId = operation.Telemetry.Context.Operation.Id;
            ParentId = operation.Telemetry.Id;
            return operation;
        }

        public void StopOperation<T>(IOperationHolder<T> operation, string operationName = "") where T : OperationTelemetry
        {
            _telemetryClient.StopOperation(operation);
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        public void TrackMetric(string eventName, double value, IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackMetric(eventName, value, properties);
        }
    }
}