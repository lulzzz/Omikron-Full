using System;
using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
    public sealed class CustomerId : ValueObject<CustomerId>
    {
        public static CustomerId New = new CustomerId(id: Guid.NewGuid().ToString());

        public CustomerId()
        {
        }

        private CustomerId(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        protected override IEnumerable<object> EqualityCheckAttributes => new List<object> {Id};

        public static CustomerId Parse(string id)
        {
            return new CustomerId(id: id.ToUpper());
        }

        public static CustomerId Parse(Guid id)
        {
            return new CustomerId(id: id.ToString().ToUpper());
        }

        private bool Equals(CustomerId other)
        {
            return base.Equals(other: other) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(objA: this, objB: obj))
            {
                return true;
            }

            return obj != null && obj.GetType() == GetType() && Equals(other: (CustomerId) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value1: base.GetHashCode(), value2: Id);
        }

        public override string ToString()
        {
            return Id;
        }

        public static implicit operator string(CustomerId id)
        {
            return id.ToString();
        }

        public static implicit operator CustomerId(string id)
        {
            return Parse(id: id);
        }

        public static bool operator ==(CustomerId left, CustomerId right)
        {
            return left?.Id == right?.Id;
        }

        public static bool operator !=(CustomerId left, CustomerId right)
        {
            return !(left == right);
        }
    }
}