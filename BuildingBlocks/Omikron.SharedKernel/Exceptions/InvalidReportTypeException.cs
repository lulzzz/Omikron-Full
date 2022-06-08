using System;

namespace Omikron.SharedKernel.Exceptions
{
    [Serializable]
    public class InvalidReportTypeException : InvalidCastException
    {
        public InvalidReportTypeException(string value) : base($"The report type '{value}' is not a valid.")
        {
        }

        public InvalidReportTypeException(int value) : this(value.ToString())
        {
        }
    }
}