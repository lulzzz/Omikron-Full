using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLogTableName : ValueObject<DataChangeLogTableName>
    {
        private readonly string _name;

        private DataChangeLogTableName(string name)
        {
            _name = name;
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> {_name};

        public static DataChangeLogTableName Parse(OmikronTenantInfo tenant)
        {
            if (tenant == null)
            {
                throw new ArgumentException(message: "The Tenant name cannot be null");
            }

            var name = GetCleanName(tenant: tenant);
            return new DataChangeLogTableName(name: name);
        }

        private static string GetCleanName(OmikronTenantInfo tenant)
        {
            return Tenant.SystemTenant.Identifier;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}