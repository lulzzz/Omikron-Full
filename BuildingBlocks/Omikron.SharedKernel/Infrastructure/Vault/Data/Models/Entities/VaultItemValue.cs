using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public abstract class VaultItemValue<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
    }
}