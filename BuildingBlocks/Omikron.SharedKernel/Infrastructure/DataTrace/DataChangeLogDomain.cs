using System;
using System.Collections.Generic;
using System.Linq;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLogDomain : ValueObject<DataChangeLogDomain>
    {
        public static DataChangeLogDomain Null = new DataChangeLogDomain("N/A");
        public static DataChangeLogDomain IdentityService = new DataChangeLogDomain("User & Role Management");
        public static DataChangeLogDomain TenantService = new DataChangeLogDomain("Tenant Management");
        public static DataChangeLogDomain SupportingService = new DataChangeLogDomain("Supporting & Bug Reporting Management");

        private readonly string _name;

        public static DataChangeLogDomain Parse(Type commandType)
        {
            if (commandType == null || string.IsNullOrWhiteSpace(commandType.FullName))
            {
                throw new ArgumentNullException(nameof(commandType), "The command type cannot be null b/c domain path will be parsed base on assembly name.");
            }

            return Domains.Where(t => commandType.FullName.StartsWith(t.Key))
                          .Select(t => t.Value)
                          .FirstOrDefault() ?? Null;
        }

        public override string ToString()
        {
            return _name;
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object>() {_name};

        private DataChangeLogDomain(string name)
        {
            _name = name;
        }

        private static Dictionary<string, DataChangeLogDomain> Domains { get; } = new Dictionary<string, DataChangeLogDomain>()
        {
            { "Omikron.IdentityService", IdentityService },
            { "Omikron.SupportingService", SupportingService },
            { "Omikron.TenantService", TenantService }
        };
    }
}