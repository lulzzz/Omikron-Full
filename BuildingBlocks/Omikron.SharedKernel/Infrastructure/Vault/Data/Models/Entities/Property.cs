using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class Property : BaseVaultItem<Guid>
    {
        public Guid? MortgageId { get; set; }
        public string Postcode { get; set; }
        public IEnumerable<PropertyValue> PropertyValues { get; set; }
        public Account Mortgage { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool AutomaticallyReValueProperty { get; set; }
        public string Address { get; set; }
    }
}
