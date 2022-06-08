using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class AssetValue : ValueObject<AssetValue>
    {
        public static AssetValue Zero = new AssetValue(value: decimal.Zero);

        private AssetValue(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { Value };


        public static AssetValue Parse(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(value), actualValue: value, message: @"The asset value should not be null or empty.");
            }

            return new AssetValue(value: value.ToDecimalOrDefault(defaultValue: decimal.Zero));
        }

        public static AssetValue Parse(int value)
        {
            return new AssetValue(value.ToDecimalOrDefault(value));
        }

        public static implicit operator AssetValue(string value)
        {
            return Parse(value: value);
        }

        public static implicit operator string(AssetValue value)
        {
            return value.ToString();
        }

        public override string ToString()
        {
            return Value.ToString(format: "N");
        }
    }
}