using System;

namespace Omikron.SharedKernel.Domain.Reporting
{
    public sealed partial class Report : BaseEntity<Guid>
    {
        public Report()
        {
            Id = Guid.NewGuid();
            RequestedAt = CreatedAt;
            State = ReportState.Awaiting;
        }

        public ReportType Type { get; set; }
        public ReportFileType FileType { get; set; }
        public ReportState State { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string InitiatedBy { get; set; }
        public string Url { get; set; }
        public string CorrelationId { get; set; }
    }
}