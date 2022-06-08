using System.Linq;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Vehicle
{
    public class ValueValueObject : BaseManualAccountDetailsVaultItem<decimal>
    {
        public ValueValueObject(Data.Models.Entities.Vehicle values) : base("Value", true)
        {
            if (values?.VehicleValues == null || !values.VehicleValues.Any())
            {
                this.ItemValue = 0m;
                return;
            }

            this.ItemValue = values.VehicleValues.OrderByDescending(x => x.CreatedAt).First().Amount;
        }
    }
}