using System;

namespace Omikron.SharedKernel.Exceptions
{
    public class SerializationException : Exception
    {
        public SerializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
