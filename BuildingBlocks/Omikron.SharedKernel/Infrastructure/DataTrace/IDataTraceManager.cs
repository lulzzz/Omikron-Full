using System.Collections.Generic;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.DataTrace.ViewModel;

namespace Omikron.SharedKernel.Infrastructure.DataTrace
{
    public interface IDataTraceManager<in TQueryModel>
    {
        Task SaveAsync(IList<DataChangeLog> dataChangeLogs);
        Task<DataChangeLogViewModel> Get(TQueryModel query);
    }
}