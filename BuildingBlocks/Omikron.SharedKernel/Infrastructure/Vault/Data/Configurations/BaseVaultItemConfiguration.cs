using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Omikron.SharedKernel.Infrastructure.Data.Configurations;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Configurations
{
    public abstract class BaseVaultItemConfiguration<TEntity, TKey> : AggregateRootTypeConfiguration<TEntity, TKey> where TEntity : BaseVaultItem<TKey> where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder: builder);

            builder
                .Ignore(propertyExpression: p => p.RowVersion);

            builder
                .Property(propertyExpression: p => p.OwnerId)
                .IsRequired()
                .HasConversion(converter: new ValueConverter<CustomerId, string>(
                    convertToProviderExpression: v => v.Id,
                    convertFromProviderExpression: v => CustomerId.Parse(v)));
        }
    }
}