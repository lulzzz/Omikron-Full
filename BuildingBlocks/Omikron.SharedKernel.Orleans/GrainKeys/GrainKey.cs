using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Orleans.GrainKeys
{
    public class GrainKey : ValueObject<GrainKey>
    {
        public GrainKey(string value)
        {
            Value = value;
        }

        public string Value { get; }
        protected override IEnumerable<object> EqualityCheckAttributes => new[] { Value };

        public static GrainKey Parse(params string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException(paramName: nameof(args));
            }

            return new GrainKey(value: string.Join(separator: "-", value: args));
        }


        public static implicit operator GrainKey(string value)
        {
            return new GrainKey(value: value);
        }

        public static implicit operator string(GrainKey value)
        {
            return value.ToString();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}