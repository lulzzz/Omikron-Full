using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Storage;
using Omikron.VaultService.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class DeleteVaultItemPhotoCommandHandler : BaseHandlerLight<DeleteVaultItemPhoto.Command, ApiResult>
	{
		private readonly IStorageProvider<Blob, Uri> _storageProvider;

		public DeleteVaultItemPhotoCommandHandler(IStorageProvider<Blob, Uri> storageProvider)
		{
			_storageProvider = storageProvider;
		}

		public async override Task<ApiResult> Handle(DeleteVaultItemPhoto.Command request, CancellationToken cancellationToken)
		{
			var deleteBlob = await _storageProvider.Delete(request.BlobName);

			return deleteBlob ? ApiResult.Success() : ApiResult.NotFound("Picture name: " + request.BlobName + " does not exist in the database.");
		}
	}
}
