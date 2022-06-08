using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
    public class InvestmentEntityTypeConfiguration : BaseVaultItemConfiguration<Investment, Guid>
    {
        public override void Configure(EntityTypeBuilder<Investment> builder)
        {
            base.Configure(builder: builder);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Category)
               .IsRequired();

            builder.Property(p => p.TickerCode)
               .IsRequired();

            builder.Property(p => p.UnitPrice)
               .IsRequired();

            builder.Property(p => p.Quantity)
               .IsRequired();

            builder.Property(p => p.TotalValue)
               .IsRequired();

            builder.ToTable(name: "Investments");
        }
    }
}