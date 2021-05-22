using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace App.Permissions
{
    internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) =>
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        // Dynamically creates a policy with a requirement that contains the permission.
        // The policy name must match the permission that is needed.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
            {
                // Policy is not for permissions, try the default provider.
                return FallbackPolicyProvider.GetPolicyAsync(policyName);
            }

            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build());
        }

    }
}