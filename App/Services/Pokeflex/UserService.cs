using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Pokeflex
{
    public class UserService
    {
        private PokeflexContext _dbContext;

        public UserService(PokeflexContext dbContext) => _dbContext = dbContext;
        
        /**
         * Purpose: Insert new user
         */
        public virtual async Task<int> Insert(AppUser appUser)
        {
            _dbContext.AppUsers.Add(appUser);
            return await _dbContext.SaveChangesAsync();
        }
        
        /**
         * Purpose: List groups a specific user is in
         */
        public virtual async Task<List<Group>> Groups(int userId) => throw new NotImplementedException();
    }
}
