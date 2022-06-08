using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using System;

namespace Omikron.IdentityService.Infrastructure.Data.Configuration
{
    public class PhoneNumberEntityTypeConfiguration : BaseEntityTypeConfiguration<PhoneNumber, Guid>
    {
        public override void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Number)
                .IsUnique(false);

            builder.HasOne(p => p.Owner)
                   .WithOne(p => p.PhoneNumberForVerification)
                   .HasForeignKey<User>(p => p.PhoneNumberId);

            builder.Property(p => p.IdentityTokenAvailable)
                   .HasDefaultValue(true);

            builder.HasData(Config.GetPhoneNumbers());
        }
    }
}
