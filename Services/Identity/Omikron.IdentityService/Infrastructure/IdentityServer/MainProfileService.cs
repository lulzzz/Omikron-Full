using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.SharedKernel.Infrastructure.Cache;
using Omikron.SharedKernel.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Storage;
using Omikron.SharedKernel.Security;

namespace Omikron.IdentityService.Infrastructure.IdentityServer
{
    public class MainProfileService : IProfileService
    {
        private readonly ICacheManager _cacheManager;
        private readonly OmikronIdentityDbContext _dbContext;
        private readonly IStorageProvider<Blob, Uri> _storageProvider;
        private readonly IdentityUserManager _userManager;

        public MainProfileService(IStorageProvider<Blob, Uri> storageProvider, IdentityUserManager userManager, OmikronIdentityDbContext dbContext, ICacheManager cacheManager)
        {
            _storageProvider = storageProvider;
            _userManager = userManager;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims = context.Subject.Claims.ToList();
            if (context.ValidatedRequest is ValidatedTokenRequest request)
            {
                var user = await _userManager.FindByNameAsync(userName: request.UserName);
                var roles = await _userManager.GetRolesAsync(user: user);
                var userStatus = (int)user.AccountStatus;

                context.IssuedClaims.Add(item: new Claim(type: ClaimTypes.Name, value: user.UserName));
                context.IssuedClaims.Add(item: new Claim(type: Claims.UserId, value: user.ExternalId.ToString()));
                context.IssuedClaims.Add(item: new Claim(type: Claims.TenantId, value: Tenant.SystemTenant.Identifier));
                context.IssuedClaims.Add(item: new Claim(type: Claims.TenantName, value: Tenant.SystemTenant.Name));
                context.IssuedClaims.Add(item: new Claim(type: Claims.AccountStatus, value: userStatus.ToString()));

                if (!string.IsNullOrWhiteSpace(value: user.PhoneNumber))
                {
                    context.IssuedClaims.Add(item: new Claim(type: ClaimTypes.MobilePhone, value: user.PhoneNumber));
                }

                if (!string.IsNullOrWhiteSpace(value: user.FirstName))
                {
                    context.IssuedClaims.Add(item: new Claim(type: JwtClaimTypes.GivenName, value: user.FirstName));
                }

                if (!string.IsNullOrWhiteSpace(value: user.LastName))
                {
                    context.IssuedClaims.Add(item: new Claim(type: JwtClaimTypes.FamilyName, value: user.LastName));
                }

                if (!string.IsNullOrWhiteSpace(value: user.ProfilePhoto))
                {
                    context.IssuedClaims.Add(item: new Claim(type: Claims.ProfilePhoto, value: user.ProfilePhoto));
                }

                if (roles.Any())
                {
                    var permissions = await _dbContext.RolePermission.AsNoTracking().Include(navigationPropertyPath: rp => rp.Role)
                        .Include(navigationPropertyPath: rp => rp.Permission)
                        .Where(predicate: r => roles.Contains(r.Role.Name) && r.Role.Enabled)
                        .Select(selector: r => r.Permission.Name)
                        .ToArrayAsync();

                    var key = $"{user.ExternalId}-{Claims.Permissions}";
                    _cacheManager.Set(key: key, @object: permissions, expired: CacheExpirationTime.UserAccountPermissionExpiration);
                }

                var token = await _storageProvider.GetAccessToken();
                context.IssuedClaims.Add(item: new Claim(type: Claims.BlobAccessToken, value: token));
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(result: true);
        }
    }
}