using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Infrastructure.Data.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(propertyExpression: p => p.ProfilePhoto).HasMaxLength(maxLength: 550);
            builder.Property(propertyExpression: p => p.PhoneNumberId).IsRequired();
        }
    }
}