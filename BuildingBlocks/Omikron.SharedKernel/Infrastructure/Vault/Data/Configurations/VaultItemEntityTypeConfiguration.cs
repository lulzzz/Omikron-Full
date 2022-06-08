using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
    public class VaultItemEntityTypeConfiguration : BaseEntityTypeConfiguration<VaultItem, Guid>
    {

        public override void Configure(EntityTypeBuilder<VaultItem> builder)
        {
            base.Configure(builder);

            builder
                .Property(propertyExpression: p => p.ItemType)
                .IsRequired()
                .HasConversion(converter: new ValueConverter<VaultItemType, int>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => VaultItemType.Parse(v)));

            builder
                .Property(propertyExpression: p => p.OwnerId)
                .IsRequired()
                .HasConversion(converter: new ValueConverter<CustomerId, string>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => CustomerId.Parse(v)));

            builder
               .Property(propertyExpression: p => p.AccountIdentificationNumber)
               .HasConversion(converter: new ValueConverter<AccountIdentificationNumber, string>(
                   convertToProviderExpression: v => v.ToString(),
                   convertFromProviderExpression: v => AccountIdentificationNumber.Parse(v)));

            builder
               .Property(p => p.AccountType)
               .HasConversion(new ValueConverter<AccountType, int>(
                   v => v.Id,
                   v => AccountType.Parse(v)));

			builder
				.Property(propertyExpression: p => p.AccountSource)
				.HasConversion(converter: new ValueConverter<AccountSource, int>(
					convertToProviderExpression: v => v.Id,
					convertFromProviderExpression: v => AccountSource.Parse(v)));

            builder
                .Property(propertyExpression: p => p.CreditDebitIndicator)
                .HasConversion(converter: new ValueConverter<CreditDebitIndicator, int>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => CreditDebitIndicator.Parse(v)));
        }
    }
}
