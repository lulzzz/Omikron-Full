using System.Threading.Tasks;
using Orleans;

namespace Omikron.SharedKernel.Orleans
{
    public interface IBaseGrainWithStringKey : IGrainWithStringKey
    {
        Task<bool> IsStateInitialized();
    }
}