using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Services
{
    public interface IHttpVaultService
    {
        Task<ApiResult> DeleteUserRelatedData(Guid userId, CancellationToken cancellationToken = default);
        Task<ApiResult<AssetPrice>> GetVehicleValue(string registration, string mileage, CancellationToken cancellationToken = default);
        Task<ApiResult<AssetPrice>> GetPropertyValue(string postcode, int numberOfBedrooms, CancellationToken cancellationToken = default);
    }
}