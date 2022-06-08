using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Data.Configurations
{
    public abstract class AggregateRootTypeConfiguration<TEntity, TKey> : BaseEntityTypeConfiguration<TEntity, TKey> where TEntity : AggregateRoot<TKey> where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder: builder);
            builder.Ignore(propertyExpression: c => c.DomainEvents);
        }
    }
}