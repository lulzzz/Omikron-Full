using Omikron.SharedKernel.Exceptions;

namespace Omikron.SharedKernel.Domain.Reporting
{
    public class ReportFileType : Enumeration
    {
        public ReportFileType()
        {
        }

        private ReportFileType(int id, string name) : base(id, name)
        {
        }

        public static ReportFileType CsvFile => new ReportFileType(1, "Csv");
        public static ReportFileType ExcelFile => new ReportFileType(2, "Excel");

        public static ReportFileType Parse(string value)
        {
            return value switch
            {
                "Csv" => CsvFile,
                "Excel" => ExcelFile,
                _ => throw new InvalidReportFileTypeException(value)
            };
        }

        public static ReportFileType Parse(int value)
        {
            return value switch
            {
                1 => CsvFile,
                2 => ExcelFile,
                _ => throw new InvalidReportFileTypeException(value)
            };
        }

        public static implicit operator string(ReportFileType value)
        {
            return value.ToString();
        }

        public static implicit operator ReportFileType(string value)
        {
            return Parse(value);
        }
    }
}