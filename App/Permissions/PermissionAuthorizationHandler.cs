using System.Data;
using System.Threading.Tasks;
using App.Models;
using App.Services.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace App.Permissions
{
    class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly AppUserManager<AppUser> _appUserManager;
        public PermissionAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _appUserManager = (AppUserManager<AppUser>)userManager;
        }

        #nullable enable
        // public virtual async Task HandleAsync(AuthorizationHandlerContext context)
        // {
        //     foreach (var req in context.Requirements.OfType<TRequirement>())
        //     {
        //         await HandleRequirementAsync(context, req);
        //     }
        // }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionsService = (PermissionService?) _appUserManager.Services.GetService(typeof(PermissionService))
                                     ?? throw new NoNullAllowedException("Null found when accessing PermissionService");
            if (await permissionsService.Permitted(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
        #nullable disable 
    }
}