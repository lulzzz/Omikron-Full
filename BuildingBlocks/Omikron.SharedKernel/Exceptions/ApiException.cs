using System;

namespace Omikron.SharedKernel.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected ApiException(string message) : base(message: message)
        {
        }
    }
}