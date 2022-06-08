using Omikron.SharedKernel.Infrastructure.Asset;

namespace Omikron.SharedKernel.Extensions
{
    public static class AssetCreationStatusExtensions
    {
        public static bool IsSuccess(this AssetCreationStatus status)
        {
            return status == AssetCreationStatus.Success;
        }
    }
}