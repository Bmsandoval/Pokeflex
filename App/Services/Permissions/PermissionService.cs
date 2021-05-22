using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Permissions
{
    public class PermissionService
    {
        private PokeflexContext _dbContext;

        public PermissionService(PokeflexContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<bool> Permitted(string permission)
        {
            return await _dbContext.AppUsers.AnyAsync();
        }
    }
}
