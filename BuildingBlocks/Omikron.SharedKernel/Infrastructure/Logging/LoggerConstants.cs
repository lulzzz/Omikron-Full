namespace Omikron.SharedKernel.Infrastructure.Logging
{
    public static class LoggerConstants
    {
        public static string CorrelationIdHeaderKey => "x-correlation-Id";

        public static string ParentIdHeaderKey => "x-parent-Id";

        public static string CorrelationId => "CorrelationId";
    }

    public static class DependencyTelemetryTypes
    {
        public const string AZURE_SERVICE_BUS = "Azure Service Bus";
    }

    public static class DependencyTelemetryNames
    {
        public const string RECEIVE = "Receive";
    }

    public static class DependencyTelemetryProperties
    {
        public const string MESSAGE_ID = "MessageId";
    }
}