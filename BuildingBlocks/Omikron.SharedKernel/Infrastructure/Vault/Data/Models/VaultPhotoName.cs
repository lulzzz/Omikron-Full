using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Models
{
	public class VaultPhotoName : ValueObject<VaultPhotoName>
	{
		private const string _prefix = "vault-item-";

		public Guid Id { get; set; }
		public string Extension { get; set; }

		private VaultPhotoName(string extension)
		{
			Id = Guid.NewGuid();
			Extension = extension;
		}

		protected override IEnumerable<object> EqualityCheckAttributes => new List<object> { Id };

		public static VaultPhotoName Parse(string extension, IConfiguration configuration)
		{
			if (extension.IsNullOrEmpty())
			{
				throw new ArgumentException("File extension cannot be null or empty.");
			}

			var allowedExtensions = configuration.GetSection("VaultPhotoSettings:AllowedFormats").Get<string[]>();

			if (!allowedExtensions.Contains(extension))
			{
				throw new ArgumentException("Invalid file extension");
			}

			return new VaultPhotoName(extension);
		}

		public override string ToString()
		{
			return $"{_prefix}{Id:N}{Extension}";
		}

		public static implicit operator string(VaultPhotoName vaultPhotoName)
		{
			return vaultPhotoName.ToString();
		}
	}
}
