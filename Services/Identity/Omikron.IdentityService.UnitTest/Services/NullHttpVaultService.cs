using IdentityModel.Client;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Security;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.UnitTest.Services
{
    public class NullHttpVaultService : IHttpVaultService
    {
        public virtual HttpClient Client { get; }

        public NullHttpVaultService(HttpClient client, ITokenService tokenService)
        {
            Client = client;
            Client.SetBearerToken(tokenService.GetToken());
        }

        public async Task<ApiResult> AddAccountsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return ApiResult.Success();
        }

        public async Task<ApiResult> DeleteUserRelatedData(Guid userId, CancellationToken cancellationToken = default)
        {
            return ApiResult.Success();
        }

		public async Task<ApiResult<AssetPrice>> GetVehicleValue(string registration, string mileage, CancellationToken cancellationToken = default)
		{
            return ApiResult<AssetPrice>.Success().WithData(new AssetPrice());
        }

		public async Task<ApiResult<AssetPrice>> GetPropertyValue(string postcode, int numberOfBedrooms, CancellationToken cancellationToken = default)
        {
            return ApiResult<AssetPrice>.Success().WithData(new AssetPrice());
        }
	}
}
