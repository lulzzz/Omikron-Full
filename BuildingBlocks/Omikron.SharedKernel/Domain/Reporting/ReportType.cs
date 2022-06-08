using Omikron.SharedKernel.Exceptions;

namespace Omikron.SharedKernel.Domain.Reporting
{
    public class ReportType : Enumeration
    {
        public ReportType()
        {
        }

        private ReportType(int id, string name) : base(id, name)
        {
        }

        public static ReportType SampleReport => new ReportType(1, "Sample report");

        public static ReportType Parse(string value)
        {
            return value switch
            {
                "Sample report" => SampleReport,
                _ => throw new InvalidReportTypeException(value)
            };
        }

        public static ReportType Parse(int value)
        {
            return value switch
            {
                1 => SampleReport,
                _ => throw new InvalidReportTypeException(value)
            };
        }

        public static implicit operator ReportType(int value)
        {
            return Parse(value);
        }

        public static implicit operator string(ReportType value)
        {
            return value.ToString();
        }

        public static implicit operator ReportType(string value)
        {
            return Parse(value);
        }

        public static implicit operator int(ReportType value)
        {
            return value.Id;
        }
    }
}