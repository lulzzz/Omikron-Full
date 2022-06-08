using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class PropertyValue : VaultItemValue<Guid>
    {
        public Guid PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
