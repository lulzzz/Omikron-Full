using System;

namespace Omikron.SharedKernel.Exceptions
{
    [Serializable]
    public class InvalidReportFileTypeException : InvalidCastException
    {
        public InvalidReportFileTypeException(string value) : base($"The report file type '{value}' is not supported.")
        {
        }

        public InvalidReportFileTypeException(int value) : this(value.ToString())
        {
        }
    }
}