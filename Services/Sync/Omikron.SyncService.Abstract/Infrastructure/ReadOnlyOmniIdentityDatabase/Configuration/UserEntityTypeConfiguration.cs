using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.Sync.Model;

namespace Omikron.Sync.Infrastructure.ReadOnlyOmikronIdentityDatabase.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: "AspNetUsers");
        }
    }
}