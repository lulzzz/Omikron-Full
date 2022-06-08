using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class Investment : BaseVaultItem<Guid>
    {
        public string Category { get; set; }
        public string TickerCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public bool AutomaticallyRevalueInvestment { get; set; }
        public IEnumerable<InvestmentValue> InvestmentValues { get; set; }
    }
}
