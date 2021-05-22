using System;
using System.Collections.Generic;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.GC;

namespace App.Permissions
{
    public class AppUserManager<TUser> : UserManager<TUser> where TUser : class
    {
        public IServiceProvider Services;

        public AppUserManager(IUserStore<TUser> store,
            IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators,
                passwordValidators, keyNormalizer, errors, services, logger)
        {
            Services = services;
        }
    }
}