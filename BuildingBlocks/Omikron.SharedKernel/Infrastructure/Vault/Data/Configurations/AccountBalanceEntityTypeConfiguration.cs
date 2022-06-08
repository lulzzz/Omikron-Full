using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
    public class AccountBalanceEntityTypeConfiguration : BaseEntityTypeConfiguration<AccountBalance, Guid>
	{
		public override void Configure(EntityTypeBuilder<AccountBalance> builder)
		{
			base.Configure(builder: builder);

			builder
				.Property(propertyExpression: p => p.CreditDebitIndicator)
				.IsRequired()
				.HasConversion(converter: new ValueConverter<CreditDebitIndicator, int>(
					convertToProviderExpression: v => v.Id,
					convertFromProviderExpression: v => CreditDebitIndicator.Parse(v)));

			builder
				.Property(propertyExpression: p => p.BalanceType)
				.IsRequired()
				.HasConversion(converter: new ValueConverter<BalanceType, int>(
					convertToProviderExpression: v => v.Id,
					convertFromProviderExpression: v => BalanceType.Parse(v)));
		}
	}
}