using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Omikron.IdentityService.Infrastructure.Data.Configuration
{
    public class PermissionEntityTypeConfiguration : AggregateRootTypeConfiguration<Permission, int>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.NormalizedName).IsRequired();
            builder.HasData(Config.GetPermissions());
        }
    }
}