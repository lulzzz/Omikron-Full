using System;

namespace Omikron.SharedKernel.Exceptions
{
    [Serializable]
    public class InvalidTenantConnectionStringTypeException : InvalidCastException
    {
        public InvalidTenantConnectionStringTypeException(string value) : base($"Tenant connection string '{value}' is not a valid.")
        {
        }

        public InvalidTenantConnectionStringTypeException(int value) : this(value.ToString())
        {
        }
    }
}