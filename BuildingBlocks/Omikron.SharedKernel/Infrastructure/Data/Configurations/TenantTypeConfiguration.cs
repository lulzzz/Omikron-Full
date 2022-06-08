using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.Data.Configurations
{
    public class TenantTypeConfiguration : AggregateRootTypeConfiguration<Tenant, string>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder: builder);

            builder.HasData(data: new List<Tenant>
            {
                Tenant.SystemTenant
            });
        }
    }
}