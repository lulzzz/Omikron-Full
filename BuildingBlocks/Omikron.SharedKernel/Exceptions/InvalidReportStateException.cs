using System;

namespace Omikron.SharedKernel.Exceptions
{
    [Serializable]
    public class InvalidReportStateException : InvalidCastException
    {
        public InvalidReportStateException(string value) : base($"The report state '{value}' is not a valid.")
        {
        }

        public InvalidReportStateException(int value) : this(value.ToString())
        {
        }
    }
}