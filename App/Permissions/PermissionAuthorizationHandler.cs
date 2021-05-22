using System;
using System.Data;
using System.Linq;
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

        // protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        // {
        //     Console.WriteLine("testypoo");
        //     var permissionsService = (PermissionService?) _appUserManager.Services.GetService(typeof(PermissionService))
        //                              ?? throw new NoNullAllowedException("Null found when accessing PermissionService");
        //     context.Succeed(requirement);
        // }

        
        #nullable enable
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionsService = (PermissionService?) _appUserManager.Services.GetService(typeof(PermissionService))
                                     ?? throw new NoNullAllowedException("Null found when accessing PermissionService");
            if (permissionsService.Permitted(requirement.Permission))
            {
                context.Succeed(requirement);
            }
            
            // if (context.User.Claims.
            //     Any(x => x.Type == "Permission" &&
            //              x.Value == requirement.Permission &&
            //              x.Issuer == "LOCAL AUTHORITY")
            // ){
            //     context.Succeed(requirement);
            // }
        }
        #nullable disable 
    }
}