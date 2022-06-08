using Omikron.SharedKernel.Infrastructure.Logging.Types;

namespace Omikron.SharedKernel.Infrastructure.Logging.Context
{
    public class LoggerContext
    {
        public LoggerContext(IPerformanceLogger performanceLogger, IUsageLogger usageLogger,
            IDiagnosticLogger diagnosticLogger, IErrorLogger errorLogger)
        {
            PerformanceLogger = performanceLogger;
            UsageLogger = usageLogger;
            DiagnosticLogger = diagnosticLogger;
            ErrorLogger = errorLogger;
        }

        public string CorrelationId
        {
            get => PerformanceLogger.CorrelationId ?? UsageLogger.CorrelationId ?? DiagnosticLogger.CorrelationId ?? ErrorLogger.CorrelationId;
            set
            {
                PerformanceLogger.CorrelationId = value;
                UsageLogger.CorrelationId = value;
                DiagnosticLogger.CorrelationId = value;
                ErrorLogger.CorrelationId = value;
            }
        }

        public string ParentId
        {
            get => PerformanceLogger.ParentId ?? UsageLogger.ParentId ?? DiagnosticLogger.ParentId ?? ErrorLogger.ParentId;
            set
            {
                PerformanceLogger.ParentId = value;
                UsageLogger.ParentId = value;
                DiagnosticLogger.ParentId = value;
                ErrorLogger.ParentId = value;
            }
        }

        public IPerformanceLogger PerformanceLogger { get; }
        public IUsageLogger UsageLogger { get; }
        public IDiagnosticLogger DiagnosticLogger { get; }
        public IErrorLogger ErrorLogger { get; }
    }
}