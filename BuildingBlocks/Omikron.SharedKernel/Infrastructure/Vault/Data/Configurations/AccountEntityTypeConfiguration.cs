using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
    public class AccountEntityTypeConfiguration : BaseVaultItemConfiguration<Account, Guid>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder: builder);

            builder
                .Property(propertyExpression: p => p.Source)
                .IsRequired()
                .HasConversion(converter: new ValueConverter<AccountSource, int>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => AccountSource.Parse(v)));

            builder
                .Property(propertyExpression: p => p.IdentificationNumber)
                .HasConversion(converter: new ValueConverter<AccountIdentificationNumber, string>(
                    convertToProviderExpression: v => v.ToString(),
                    convertFromProviderExpression: v => AccountIdentificationNumber.Parse(v)));

            builder
                .Property(propertyExpression: p => p.LoanType)
                .HasConversion(converter: new ValueConverter<LoanType, int>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => LoanType.Parse(v)));

            builder
                .Property(propertyExpression: p => p.Type)
                .HasConversion(converter: new ValueConverter<AccountType, int>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => AccountType.Parse(v)));

            builder.ToTable(name: "Accounts");
        }
    }
}