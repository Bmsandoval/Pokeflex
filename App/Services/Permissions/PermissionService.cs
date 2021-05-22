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

        public virtual bool Permitted(string permission)
        {
            // var users = (DbSet<IdentityUser<int>>) _dbContext.AppUsers;
            // return _dbContext.AppUsers.AnyAsync(x => x.Type == "Permission" &&
            //              x.Value == requirement.Permission &&
            //              x.Issuer == "LOCAL AUTHORITY")
            
            // return false;
            return true;
        }
    }
}
