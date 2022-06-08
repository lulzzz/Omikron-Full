using System;
using Omikron.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Omikron.SharedKernel.Infrastructure.Data.Configurations
{
    public abstract class BaseEntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(p => p.RowVersion)
                .IsRowVersion();

            builder
                .Property(p => p.CreatedAt)
                .IsRequired();
        }
    }


    public abstract class BaseEntityTypeConfigurationNoVersioning<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Ignore(p => p.RowVersion);

            builder
                .Property(p => p.CreatedAt)
                .IsRequired();
        }
    }
}