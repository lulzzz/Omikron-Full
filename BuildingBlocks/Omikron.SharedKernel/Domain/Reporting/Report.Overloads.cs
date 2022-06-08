using System.Globalization;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Domain.Reporting
{
    public sealed partial class Report
    {
        public Report WithCorrelationId(string correlationId)
        {
            CorrelationId = correlationId;
            return this;
        }

        public Report WithUrl(string url)
        {
            Url = url;
            return this;
        }

        public Report ToProcessing()
        {
            State = ReportState.Processing;
            return this;
        }

        public Report ToFail()
        {
            State = ReportState.Fail;
            return this;
        }

        public Report ToCompleted()
        {
            State = ReportState.Complete;
            CompletedAt = Clock.GetTime();
            return this;
        }

        public override string ToString()
        {
            var time = Clock.GetTime().ToString("u", DateTimeFormatInfo.InvariantInfo);
            return $"{Type}-{time}";
        }

        public static implicit operator string(Report report)
        {
            return report.ToString();
        }
    }
}