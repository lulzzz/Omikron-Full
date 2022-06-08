using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public class DataChangeLogPartitionKey : ValueObject<DataChangeLogPartitionKey>
    {
        private readonly string _username;

        private DataChangeLogPartitionKey(string username)
        {
            _username = username;
        }

        public static DataChangeLogPartitionKey Parse(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The username cannot be empty or null.");
            }

            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(username))
            {
                throw new ArgumentException("The username must be a valid email address of user.");
            }

            return new DataChangeLogPartitionKey(username);
        }

        public override string ToString()
        {
            return _username;
        }

        protected override IEnumerable<object> EqualityCheckAttributes => new List<object>() {_username};
    }
}