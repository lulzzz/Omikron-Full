using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class VehicleValue : VaultItemValue<Guid>
    {
        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
