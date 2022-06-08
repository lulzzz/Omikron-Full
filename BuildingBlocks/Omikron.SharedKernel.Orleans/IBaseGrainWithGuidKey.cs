using System.Threading.Tasks;
using Orleans;

namespace Omikron.SharedKernel.Orleans
{
    public interface IBaseGrainWithGuidKey : IGrainWithGuidKey
    {
        Task<bool> IsStateInitialized();
    }
}