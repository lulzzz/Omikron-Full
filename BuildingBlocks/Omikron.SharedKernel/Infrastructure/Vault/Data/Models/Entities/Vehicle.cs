using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities
{
    public class Vehicle : BaseVaultItem<Guid>
    {
        public Guid? FinancialAgreementId { get; set; }
        public string Registration { get; set; }
        public string Mileage { get; set; }
        public bool AutomaticallyReValueVehicle { get; set; }

        public Account FinancialAgreement { get; set; }
        public IEnumerable<VehicleValue> VehicleValues { get; set; }
    }
}
