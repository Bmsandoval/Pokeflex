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
    public class PokeflexService
    {
        private PokeflexContext _dbContext;

        public PokeflexService(PokeflexContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Pokemon> Select(int pkNumber, int? flexGroup=null)
        {
            var pokeCtx = _dbContext.Pokemons;
            return await pokeCtx
                .Flexmons(flexGroup,pkNumber)
                .IncludeBasemons(pokeCtx,flexGroup,pkNumber)
                .FirstOrDefaultAsync();
        }
        
        public virtual async Task<int> Insert(Pokemon pokemon)
        {
            _dbContext.Pokemons.Add(pokemon);
            return await _dbContext.SaveChangesAsync();
        }
        
        public virtual async Task<int> Update(int? pkGroup, int pkNumber, Pokemon pokemon)
        {
            var pokeCtx = _dbContext.Pokemons;
            var pk = await pokeCtx
                .Flexmons(pkGroup,pkNumber)
                .IncludeBasemons(pokeCtx,pkGroup,pkNumber)
                .FirstOrDefaultAsync();
            if (pk is null) { return default; }

            pk.GroupId = pokemon.GroupId;
            pk.Number = pokemon.Number;
            pk.Name = pokemon.Name;

            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Pokemon>> GetRange(int offset, int limit, int? flexGroup=null)
        {
            var pokeCtx = _dbContext.Pokemons;
            return await pokeCtx
                .Flexmons(flexGroup,offset, limit)
                .IncludeBasemons(pokeCtx,flexGroup,offset, limit)
                .OrderByNum()
                .ToListAsync();
        }
        
        public virtual async Task<List<User>> Testy()
        {
            // var mathClass = new Class { Name = "Math" };
            // mathClass.Students.Add(new Student { Name = "Alice" });
            // mathClass.Students.Add(new Student { Name = "Bob" });
            // context.AddToClasses(mathClass);
            // context.SaveChanges();
            // return await _dbContext.Users
            //     .Include(u => u.UserGroups)
            //     .ThenInclude(ug => ug.Group)
            //     .ToListAsync();

            // var usergroup = new UserGroup();
            // usergroup.User = new User(); 
            // usergroup.Group = new Group();
            // usergroup.Group.Pokemons = new List<Pokemon>
            // {
            //     new () {
            //         ApiSource = "1",
            //         GroupId = 1,
            //         Name = "testy",
            //         Number = 1
            //     },
            // };
            // _dbContext.UserGroups.Add(usergroup);
            // await _dbContext.SaveChangesAsync();
            // return await _dbContext.Users.ToListAsync();
            // // .Include(u => u.UserGroups)
            // // .ThenInclude(ug => ug.Group)
            // // .ToListAsync();
            
            var user = new User();
            var usergroup = new UserGroup();
            var group = new Group();
            var pokemon = new Pokemon();
            
            user.UserGroups = new List<UserGroup>{usergroup};
            group.UserGroups=new List<UserGroup>{usergroup};
            group.Pokemons = new List<Pokemon>{pokemon};
            
            _dbContext.UserGroups.Add(usergroup);
            _dbContext.Users.Add(user);
            _dbContext.Groups.Add(group);
            _dbContext.Pokemons.Add(pokemon);
            
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .ToListAsync();
        }
    }
}
