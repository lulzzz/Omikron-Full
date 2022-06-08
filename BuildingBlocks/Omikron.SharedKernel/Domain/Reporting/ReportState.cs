using Omikron.SharedKernel.Exceptions;

namespace Omikron.SharedKernel.Domain.Reporting
{
    public class ReportState : Enumeration
    {
        public ReportState()
        {
        }

        private ReportState(int id, string name) : base(id, name)
        {
        }

        public static ReportState Awaiting => new ReportState(1, "Awaiting");
        public static ReportState Processing => new ReportState(2, "Processing");
        public static ReportState Complete => new ReportState(3, "Complete");
        public static ReportState Fail => new ReportState(4, "Fail");

        public static ReportState Parse(string value)
        {
            return value switch
            {
                "Awaiting" => Awaiting,
                "Processing" => Processing,
                "Completed" => Complete,
                "Fail" => Fail,
                _ => throw new InvalidReportStateException(value)
            };
        }

        public static ReportState Parse(int value)
        {
            return value switch
            {
                1 => Awaiting,
                2 => Processing,
                3 => Complete,
                4 => Fail,
                _ => throw new InvalidReportStateException(value)
            };
        }

        public static implicit operator ReportState(int value)
        {
            return Parse(value);
        }

        public static implicit operator int(ReportState value)
        {
            return value.Id;
        }
    }
}