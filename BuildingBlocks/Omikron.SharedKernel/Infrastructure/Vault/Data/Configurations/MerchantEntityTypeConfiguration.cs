using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
	public class MerchantEntityTypeConfiguration : BaseEntityTypeConfiguration<Merchant, Guid>
    {
		public override void Configure(EntityTypeBuilder<Merchant> builder)
		{
			builder
				.Property(p => p.Name)
				.IsRequired();

			builder
				.HasIndex(p => p.Name)
				.IsUnique();

			builder
				.HasMany(p => p.Transactions)
				.WithOne(p => p.Merchant)
				.HasForeignKey(p => p.MerchantId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			base.Configure(builder);
		}
	}
}
