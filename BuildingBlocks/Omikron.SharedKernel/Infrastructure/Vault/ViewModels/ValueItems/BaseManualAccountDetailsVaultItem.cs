using Omikron.SharedKernel.Domain;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems
{
    public abstract class BaseManualAccountDetailsVaultItem<T> : ValueObject<BaseManualAccountDetailsVaultItem<T>>, IBaseManualAccountDetailsVaultItem
    {
        protected BaseManualAccountDetailsVaultItem(string name, T  value, bool renderCurrency)
        {
            Name = name;
            ItemValue = value;
            RenderCurrency = renderCurrency;
        }

        protected BaseManualAccountDetailsVaultItem(string name, bool renderCurrency)
        {
            Name = name;
            RenderCurrency = renderCurrency;
        }

        public string Name { get; set; }
        public T ItemValue { get; set; }
        public bool RenderCurrency { get; set; }
        public object Value => ItemValue;

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { Name, ItemValue, RenderCurrency };
    }
}