using FluentValidation;
using Microsoft.Extensions.Configuration;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.VaultService.Domain.Commands
{
    public class DeleteVaultItemPhoto
    {
        public class Command : TenantCommand<ApiResult>
		{
			public Command(string blobName)
			{
                BlobName = blobName;
            }

			public string BlobName { get; set; }
		}

        public class Validation : AbstractValidator<Command>
        {
            public Validation(IConfiguration configuration)
            {
                RuleFor(x => x.BlobName).NotEmpty().WithMessage("Picture can't be empty!");
            }
        }
    }
}
