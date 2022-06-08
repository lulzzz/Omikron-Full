using Microsoft.Extensions.Configuration;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Storage;
using Omikron.VaultService.Domain.Commands;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.VaultService.Domain.Handlers
{
	public class AddVaultItemPhotoCommandHandler : BaseHandlerLight<AddVaultItemPhoto.Command, ApiResult<Uri>>
    {
		private readonly IStorageProvider<Blob, Uri> _storageProvider;
		private readonly IConfiguration _configuration;

		public AddVaultItemPhotoCommandHandler(IStorageProvider<Blob, Uri> storageProvider, IConfiguration configuration)
		{
			_storageProvider = storageProvider;
			_configuration = configuration;
		}

		public async override Task<ApiResult<Uri>> Handle(AddVaultItemPhoto.Command request, CancellationToken cancellationToken)
		{
			var extension = Path.GetExtension(request.PhotoFile.FileName);
			var name = VaultPhotoName.Parse(extension, _configuration);

			using var stream = request.PhotoFile.OpenReadStream();
			var blob = new Blob(name, stream);
			var uri = await _storageProvider.SaveAsync(blob);

			return ApiResult<Uri>.Success().WithData(uri);
		}
	}
}
