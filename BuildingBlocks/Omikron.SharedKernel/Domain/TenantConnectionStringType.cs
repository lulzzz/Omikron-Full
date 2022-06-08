using Omikron.SharedKernel.Exceptions;

namespace Omikron.SharedKernel.Domain
{
    public class TenantConnectionStringType : Enumeration
    {
        public TenantConnectionStringType()
        {
        }

        private TenantConnectionStringType(int id, string name) : base(id, name)
        {
        }

        public static TenantConnectionStringType IdentityConnectionString => new TenantConnectionStringType(1, Constants.IdentityConnectionString);
        public static TenantConnectionStringType ReportingConnectionString => new TenantConnectionStringType(2, Constants.ReportingConnectionString);

        public static TenantConnectionStringType Parse(string value)
        {
            return value switch
            {
                Constants.IdentityConnectionString => IdentityConnectionString,
                Constants.ReportingConnectionString => ReportingConnectionString,
                _ => throw new InvalidTenantConnectionStringTypeException(value)
            };
        }

        public static TenantConnectionStringType Parse(int value)
        {
            return value switch
            {
                1 => IdentityConnectionString,
                2 => ReportingConnectionString,
                _ => throw new InvalidTenantConnectionStringTypeException(value)
            };
        }

        public static implicit operator string(TenantConnectionStringType value)
        {
            return value.ToString();
        }

        public static implicit operator TenantConnectionStringType(string value)
        {
            return Parse(value);
        }
    }
}
