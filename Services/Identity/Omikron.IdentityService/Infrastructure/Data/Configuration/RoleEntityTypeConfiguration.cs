using Omikron.IdentityService.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Omikron.IdentityService.Infrastructure.Data.Configuration
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.HasData(Config.GetRoles());
        }
    }
}