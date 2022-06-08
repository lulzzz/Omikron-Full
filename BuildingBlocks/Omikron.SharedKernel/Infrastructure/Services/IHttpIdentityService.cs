using Omikron.SharedKernel.Api.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Services
{
	public interface IHttpIdentityService
	{
		Task<ApiResult> CreateUserAccount<TRequest>(TRequest request, string tenantId);
		Task<ApiResult> DeleteAccount<TResponse>(Guid userId, string tenantId);
		Task<ApiResult<TResponse>> GetUserByUsername<TResponse>(string username, CancellationToken cancellationToken = default);
		Task<ApiResult<TResponse>> GetUserRegistrationDate<TResponse>(CancellationToken cancellationToken = default);
		Task<ApiResult<TResponse>> GetUsersByRoles<TResponse>(string[] roles, string tenantId);
		Task<ApiResult> UpdateUserAccount<TRequest>(TRequest request, string tenantId);
	}
}