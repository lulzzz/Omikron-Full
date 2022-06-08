using System.Collections.Generic;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public sealed class AccountIdentificationNumber : ValueObject<AccountIdentificationNumber>
	{
		private const string _divider = "•";
		public string SortCode { get; set; }
		public string AccountNumber { get; set; }
		protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { AccountNumber };

		private AccountIdentificationNumber(string sortCode, string accountNumber)
		{
			SortCode = sortCode;
			AccountNumber = accountNumber;
		}

		public AccountIdentificationNumber(string identificationNumber)
		{
			if (identificationNumber.IsNotNullOrEmpty() && identificationNumber.Length > Constants.SortCodeLength)
			{
				SortCode = identificationNumber.Substring(0, Constants.SortCodeLength);
				AccountNumber = identificationNumber[Constants.SortCodeLength..];
			}
			else
			{
				AccountNumber = identificationNumber;
			}
		}

		public static AccountIdentificationNumber Parse(string identificationNumber)
		{
			if (!identificationNumber.Contains(_divider))
			{
				return new AccountIdentificationNumber(identificationNumber);
			}

			var parts = identificationNumber.Split(_divider);
			return new AccountIdentificationNumber(parts[0], parts[1]);
		}

		public static implicit operator string(AccountIdentificationNumber id)
		{
			return id?.ToString();
		}

		public override string ToString()
		{
			return SortCode.IsNullOrEmpty() ? AccountNumber : $"{SortCode}{_divider}{AccountNumber}";
		}
	}
}
