using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
    public class TransactionEntityTypeConfiguration : BaseEntityTypeConfiguration<Transaction, Guid>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder
                .Property(propertyExpression: p => p.CreditDebitIndicator)
                .HasConversion(converter: new ValueConverter<CreditDebitIndicator, int>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => CreditDebitIndicator.Parse(v)));

            builder
               .Property(propertyExpression: p => p.TransactionStatus)
               .HasConversion(converter: new ValueConverter<TransactionStatus, int>(
                   convertToProviderExpression: v => v.Id,
                   convertFromProviderExpression: v => TransactionStatus.Parse(v)));


            builder
                .HasOne(p => p.Merchant)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.MerchantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
