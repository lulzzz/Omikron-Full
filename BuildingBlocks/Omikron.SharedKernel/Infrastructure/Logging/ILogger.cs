using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Logging
{
    public interface ILogger : ITrackingLogger
    {
        void Debug(string message);
        void Debug<TData>(string message, TData data);
        void Error(Exception exception);
        void Error<TData>(Exception exception, TData data);
        void Error(string message);
        void Error<TData>(string message, TData data);
        void Fatal(Exception exception);
        void Fatal<TData>(Exception exception, TData data);
        void Information(string message);
        void Information<TData>(string message, TData data);
        void Warning(string message);
        void Warning<TData>(string message, TData data);

        IOperationHolder<T> StartOperation<T>(T operationTelemetry) where T : OperationTelemetry;
        void StopOperation<T>(IOperationHolder<T> operation, string operationName = "") where T : OperationTelemetry;
        Task<TResult> TrackOperation<TResult>(Func<Task<TResult>> operation, string name, IDictionary<string, string> customProperties = default(IDictionary<string, string>));
        TResult TrackDependency<TResult>(Func<TResult> operation, string dependencyType, string dependencyName, string dependencyCommand);
        void TrackDependency(Action operation, string dependencyType, string dependencyName, string dependencyCommand);
        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        void TrackMetric(string eventName, double value, IDictionary<string, string> properties = null);
    }
}