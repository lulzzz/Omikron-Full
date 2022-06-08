using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Types;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Omikron.SharedKernel.Infrastructure.Logging.Implementations
{
    public class NullLogger : ILogger, IPerformanceLogger, IUsageLogger, IErrorLogger, IDiagnosticLogger
    {
        public string CorrelationId { get; set; }
        
        public string ParentId { get; set; }

        public void Debug(string message)
        {
        }

        public void Debug<TData>(string message, TData data)
        {
        }

        public void Error(Exception exception)
        {
        }

        public void Error<TData>(Exception exception, TData data)
        {
        }

        public void Error(string message)
        {
        }

        public void Error<TData>(string message, TData data)
        {
        }

        public void Fatal(Exception exception)
        {
        }

        public void Fatal<TData>(Exception exception, TData data)
        {
        }

        public void Information(string message)
        {
        }

        public void Information<TData>(string message, TData data)
        {
        }

        public void Warning(string message)
        {
        }

        public void Warning<TData>(string message, TData data)
        {
        }


        public TResult TrackDependency<TResult>(Func<TResult> operation, string dependencyType, string dependencyName, string dependencyCommand)
        {
            return default(TResult);
        }

        public void TrackDependency(Action operation, string dependencyType, string dependencyName, string dependencyCommand)
        {

        }

        public TResult TrackOperation<TResult>(Func<TResult> operation, string name)
        {
            return default(TResult);
        }

        public IOperationHolder<T> StartOperation<T>(T operationTelemetry) where T : OperationTelemetry
        {
            return default(IOperationHolder<T>);
        }

        public void StopOperation<T>(IOperationHolder<T> operation, string operationName = "") where T : OperationTelemetry
        {

        }

        public Task<TResult> TrackOperation<TResult>(Func<Task<TResult>> operation, string name, IDictionary<string, string> customProperties = null)
        {
            return Task.FromResult(default(TResult));
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
        }

        public void TrackMetric(string eventName, double value, IDictionary<string, string> properties = null)
        {
        }
    }
}