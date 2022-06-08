using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class InvestmentValue : VaultItemValue<Guid>
    {
        public Guid InvestmentId { get; set; }

        public Investment Investment { get; set; }
    }
}
