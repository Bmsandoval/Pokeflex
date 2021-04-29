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
    public class GroupService
    {
        private PokeflexContext _dbContext;

        public GroupService(PokeflexContext dbContext) => _dbContext = dbContext;

        /**
         * Purpose: Insert new group
         */
        public virtual async Task<int> Insert(Group group)
        {
            _dbContext.Groups.Add(group);
            return await _dbContext.SaveChangesAsync();
        }
        
        /**
         * Purpose: List all groups
         */
        public virtual async Task<List<Group>> List()
        {
            return await _dbContext.Groups.ToListAsync();
        }
        
        /**
         * Purpose: Add user to a group
         */
        public virtual async Task<int> AddUser(int groupId, User user) => throw new NotImplementedException();
        
        /**
         * Purpose: List users in a specific group
         */
        public virtual async Task<List<User>> ListUsers(int groupId) => throw new NotImplementedException();
    }
}
