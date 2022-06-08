using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public abstract class BaseVaultItem<TKey> : AggregateRoot<TKey> where TKey : IEquatable<TKey>
    {
        public CustomerId OwnerId { get; set; }
        public Guid ExternalId { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsArchived { get; set; }
    }
}
