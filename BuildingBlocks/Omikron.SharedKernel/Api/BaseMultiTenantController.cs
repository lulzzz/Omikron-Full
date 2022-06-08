using System;
using System.Threading.Tasks;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Api
{
    public abstract class BaseMultiTenantController : Controller
    {
        protected readonly IDispatcher Dispatcher;
        private readonly ITenantAccessor _tenantAccessor;
        protected OmikronTenantInfo Tenant  => _tenantAccessor.GetTenant();
        protected string Username => User.Identity.Name;

        protected BaseMultiTenantController(IDispatcher dispatcher, IServiceProvider serviceProvider)
        {
            Dispatcher = dispatcher;
            _tenantAccessor = serviceProvider.GetService<ITenantAccessor>();
        }

        protected virtual async Task<IActionResult> SendTenantCommandAsync(TenantCommand<ApiResult> command)
        {
            return await ExecuteAsync<TenantCommand<ApiResult>, ApiResult>(command);
        }

        protected virtual async Task<IActionResult> SendTenantCommandAsync<TResponse>(TenantCommand<ApiResult<TResponse>> command)
        {
            return await ExecuteAsync<TenantCommand<ApiResult<TResponse>>, ApiResult<TResponse>>(command);
        }

        private async Task<IActionResult> ExecuteAsync<TCommand, TResponse>(TCommand command) where TCommand : TenantCommand<TResponse> where TResponse : ApiResult
        {
            command.TenantIdentifier = Tenant.Identifier;
            var result = await Dispatcher.DispatchAsync(command: command);
            return result.ToActionResult();
        }
    }
}