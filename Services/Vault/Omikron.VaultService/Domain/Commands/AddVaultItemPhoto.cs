using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;
using System.IO;
using System.Linq;

namespace Omikron.VaultService.Domain.Commands
{
	public class AddVaultItemPhoto
    {
        public class Command : TenantCommand<ApiResult<Uri>>
		{
			public Command(IFormFile photoFile)
			{
				PhotoFile = photoFile;
			}

			public IFormFile PhotoFile { get; set; }
		}

        public class Validation : AbstractValidator<Command>
        {
            public Validation(IConfiguration configuration)
            {
                var maxSize = configuration.GetValue<long>("VaultPhotoSettings:MaxSize");
                var allowedFormats = configuration.GetSection("VaultPhotoSettings:AllowedFormats").Get<string[]>();

                RuleFor(x => x.PhotoFile).NotNull();

                RuleFor(x => x.PhotoFile.Length).LessThanOrEqualTo(maxSize).WithMessage(
                    $"The image size has been exceeded. The maximum image size can be {maxSize / 1024 / 1024} MB.");

                RuleFor(x => x.PhotoFile.FileName).Must(fileName => allowedFormats.Contains(Path.GetExtension(fileName)))
                    .WithMessage(
                        $"The format of images is invalid. Please choose one of the next allowed formats: {string.Join(", ", allowedFormats)}.");
            }
        }
    }
}
