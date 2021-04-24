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

        public virtual async Task<Pokemon> Select(int pkNumber, int flexGroup=0)
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
        
        public virtual async Task<int> Update(int pkGroup, int pkNumber, Pokemon pokemon)
        {
            var pokeCtx = _dbContext.Pokemons;
            var pk = await pokeCtx
                .Flexmons(pkGroup,pkNumber)
                .IncludeBasemons(pokeCtx,pkGroup,pkNumber)
                .FirstOrDefaultAsync();
            // var pk = await Select(pkNumber, pkGroup);
            if (pk.Equals(default)) { return default; }

            pk.Group = pokemon.Group;
            pk.Number = pokemon.Number;
            pk.Name = pokemon.Name;

            return await _dbContext.SaveChangesAsync();
        }
        
        public virtual async Task<List<Pokemon>> GetRange(int offset, int limit, int flexGroup=0)
        {
            var pokeCtx = _dbContext.Pokemons;
            return await pokeCtx
                .Flexmons(flexGroup,offset, limit)
                .IncludeBasemons(pokeCtx,flexGroup,offset, limit)
                .OrderByNum()
                .ToListAsync();
        }
    }
}
