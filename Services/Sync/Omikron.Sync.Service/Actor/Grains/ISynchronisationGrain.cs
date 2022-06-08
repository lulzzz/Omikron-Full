using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Orleans;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Service.Actor.Grains
{
	public interface ISynchronisationGrain<T> : IBaseGrainWithGuidKey
	{
		Task<Result> InitializeEntityAsync(T entity);
		Task<Result> Sync(CancellationToken cancellationToken);
	}
}
