using System;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Queries;
using Omikron.SharedKernel.Api;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Omikron.IdentityService.Controllers
{
    [AuthorizeByTenantAndCredentials]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    public class UserController : BaseMultiTenantController
    {
        private readonly IServiceProvider _serviceProvider;

        public UserController(IDispatcher dispatcher, IServiceProvider serviceProvider) : base(dispatcher, serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("{userId}/permissions"), SwaggerOperation(OperationId = "GetPermissionsByUserAccountId.Query")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> GetPermissionsByUserAccountId(Guid userId)
        {
            var result = await Dispatcher.DispatchAsync(new GetPermissionsByUserAccountId.Query(userId));
            return result.ToActionResult();
        }

        [HttpGet("{userId}"), SwaggerOperation(OperationId = "GetUserAccountByUserAccountId.Query")]
        [AuthorizeByPermissions(PermissionConstants.UserManagement.ReadUser, PermissionConstants.UserManagement.EditUser)]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        public async Task<IActionResult> GetUserAccountByUserAccountId(Guid userId)
        {
            var result = await Dispatcher.DispatchAsync(new GetUserAccountByUserAccountId.Query(userId));
            return result.ToActionResult();
        }

        [HttpPost, SwaggerOperation(OperationId = "AddUserAccount.Command")]
        [AuthorizeByPermissions(PermissionConstants.UserManagement.AddUser)]
        [SwaggerResponse(204, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        public async Task<IActionResult> AddUserAccount(AddUserAccount.Command command)
        {
            return await SendTenantCommandAsync(command);
        }

        [HttpPut("basic-information"), SwaggerOperation(OperationId = "UpdateUserAccountBasicInformationByUsername.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        [AuthorizeByPermissions(PermissionConstants.UserManagement.EditUser)]
        public async Task<IActionResult> UpdateUserAccountBasicInformationByUsername(UpdateUserAccountBasicInformationByUsername.Command command)
        {
            command.Username = User.Identity.Name;
            return await SendTenantCommandAsync(command);
        }

        [HttpPut("{userId}/basic-information"), SwaggerOperation(OperationId = "UpdateUserAccountBasicInformationByUserAccountId.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        [AuthorizeByPermissions(PermissionConstants.UserManagement.EditUser)]
        public async Task<IActionResult> UpdateUserAccountBasicInformationByUserAccountId(Guid userId, UpdateUserAccountBasicInformationByUserAccountId.Command command)
        {
            command.Id = userId;
            return await SendTenantCommandAsync(command);
        }

        [HttpPut("{userId}/account-status"), SwaggerOperation(OperationId = "UpdateUserAccountStatus.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        [AuthorizeByPermissions(PermissionConstants.UserManagement.EditUser)]
        public async Task<IActionResult> UpdateUserAccountStatus(Guid userId, UpdateUserAccountStatus.Command command)
        {
            command.UserId = userId;
            return await SendTenantCommandAsync(command);
        }

        [HttpDelete("{userId}/{verificationCode}/{isAdmin}"), SwaggerOperation(OperationId = "DeleteUserAccount.Command")]
        [SwaggerResponse(204, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp404)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [AuthorizeByPermissions(PermissionConstants.UserManagement.DeleteUser)]
        public async Task<IActionResult> DeleteUserAccount(Guid userId, int verificationCode, bool isAdmin)
        {
            var command = new DeleteUserAccount.Command(userId, verificationCode, isAdmin);
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPost("create"), SwaggerOperation(OperationId = "CreateUser.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp404)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> CreateUser(CreateUser.Command command)
        {
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPut("reset-password"), SwaggerOperation(OperationId = "ChangeUserAccountPasswordRequest.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp404)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> ChangeUserAccountPasswordRequest(ChangeUserAccountPasswordRequest.Command command)
        {
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPut("change-password"), SwaggerOperation(OperationId = "ChangeUserAccountPasswordByToken.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> ChangeUserAccountPasswordByToken(ChangeUserAccountPasswordByToken.Command command)
        {
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPost("change-password/token-verify"), SwaggerOperation(OperationId = "VerifyUserAccountResetPasswordToken.Query")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp404)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        public async Task<IActionResult> VerifyUserAccountResetPasswordToken(VerifyUserAccountResetPasswordToken.Query query)
        {
            return await SendTenantCommandAsync(query);
        }

        [AllowAnonymous]
        [HttpPost("confirm-email/token-verify"), SwaggerOperation(OperationId = "VerifyUserAccountConfirmationEmailToken.Query")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        public async Task<IActionResult> VerifyUserAccountConfirmationEmailToken(VerifyUserAccountConfirmationEmailToken.Query command)
        {
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPut("confirm-email"), SwaggerOperation(OperationId = "ConfirmUserAccountEmailByToken.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> ConfirmUserAccountEmailByToken(ConfirmUserAccountEmailByToken.Command command)
        {
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPut("reset-email-token"), SwaggerOperation(OperationId = "ResetEmailToken.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> ResetEmailToken(ResetEmailToken.Command command)
        {
            command.TenantIdentifier = Tenant.Identifier;

            return await SendTenantCommandAsync(command);
        }

        [HttpPut("{userId}/reset-email-token"), SwaggerOperation(OperationId = "ResetEmailTokenByUserId.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        [SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
        public async Task<IActionResult> ResetEmailTokenByUserId(Guid userId)
        {
            return await SendTenantCommandAsync(new ResetEmailTokenByUserId.Command(userId, Tenant.Identifier));
        }

        [AllowAnonymous]
        [HttpPost("phone-number"), SwaggerOperation(OperationId = "AddPhoneNumber.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumber.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }
        
        [HttpPut("kyc"), SwaggerOperation(OperationId = "PerformKyc.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> PerformKyc(PerformKyc.Command command)
        {
            command.UserName = Username;
            return await SendTenantCommandAsync(command);
        }

        [HttpPost("open-banking-login"), SwaggerOperation(OperationId = "OpenBankingLogin.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        [SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
        public async Task<IActionResult> OpenBankingLogin(OpenBankingLogin.Command command)
        {
            command.UserName = Username;
            return await SendTenantCommandAsync(command);
        }

        [AllowAnonymous]
        [HttpPut("generate-token"), SwaggerOperation(OperationId = "GenerateTokenForUser.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> GenerateToken(GenerateTokenForUser.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPut("password-reset"), SwaggerOperation(OperationId = "PasswordChange.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> PasswordChange(ResetPassword.Command command)
        {
            return await SendTenantCommandAsync(command);
        }

        [HttpGet("list-ob-providers"), SwaggerOperation(OperationId = "GetListOfObProviders.Query")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> ListObProviders([FromQuery] string search)
        {
            var result = await Dispatcher.DispatchAsync(new GetListOfObProviders.Query(search));
            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPost("login"), SwaggerOperation(OperationId = "Login.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> Login(Login.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPost("login-verification"), SwaggerOperation(OperationId = "LoginVerification.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> LoginVerification(LoginVerification.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpPut("generate-token-for-new-number"), SwaggerOperation(OperationId = "GenerateTokenForNewNumber.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> GenerateTokenForNewNumber(GenerateTokenForNewNumber.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpPut("edit-profile-details"), SwaggerOperation(OperationId = "EditProfileDetails.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> EditProfileDetails(EditProfileDetails.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpGet("profile-details/{userId}"), SwaggerOperation(OperationId = "GetProfileDetails.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> GetProfileDetails([FromRoute] Guid userId)
        {
            var result = await Dispatcher.DispatchAsync(new GetProfileDetails.Query(userId));
            return result.ToActionResult();
        }

        [HttpPut("update-marketing"), SwaggerOperation(OperationId = "UpdateMarketingCommunications.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> UpdateMarketingCommunications(UpdateMarketingCommunications.Command command)
        {
            command.Username = Username;
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpPut("update-notifications"), SwaggerOperation(OperationId = "UpdateNotifications.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> UpdateNotifications(UpdateNotifications.Command command)
        {
            command.Username = Username;
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpPut("generate-token-to-change-password"), SwaggerOperation(OperationId = "GenerateTokenForNewPassword.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> GenerateTokenToChangePassword(GenerateTokenForNewPassword.Command command)
        {
            command.Username = Username;
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpPut("password-change"), SwaggerOperation(OperationId = "ChangePassword.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> PasswordChange(ChangePassword.Command command)
        {
            command.Username = Username;
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpPut("remember-me"), SwaggerOperation(OperationId = "ActivateRememberMe.Command")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> ActivateRememberMe(ActivateRememberMe.Command command)
        {
            var result = await Dispatcher.DispatchAsync(command);
            return result.ToActionResult();
        }

        [HttpGet("registration-date"), SwaggerOperation(OperationId = "GetUserRegistrationDate.Query")]
        [SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
        [SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
        public async Task<IActionResult> GetUserRegistrationDate()
        {
            var result = await Dispatcher.DispatchAsync(new GetUserRegistrationDate.Query(Username));
            return result.ToActionResult();
        }
    }
}