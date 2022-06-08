using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property
{
    public class ValueValueObject : BaseManualAccountDetailsVaultItem<decimal>
    {
        public ValueValueObject(Data.Models.Entities.Property values) : base("Value", true)
        {
            if (values?.PropertyValues == null || !values.PropertyValues.Any())
            {
                this.ItemValue = 0m;
                return;
            }

            this.ItemValue = values.PropertyValues.OrderByDescending(x => x.CreatedAt).First().Amount;
        }
    }
}