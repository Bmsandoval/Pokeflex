using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<Pokemon> Select(int pkNumber, int pkGroup=0)
        {
            if (pkNumber < 1) {return default;}
            
            IQueryable<Pokemon> flexPokemon = from f in _dbContext.Pokemons where f.Group == pkGroup && f.Number == pkNumber select f;
            IQueryable<Pokemon> basePokemon = from b in _dbContext.Pokemons where b.Group == 0 && b.Number == pkNumber select b;
            return await flexPokemon
                .Concat(
                    from p in basePokemon where !(
                        from f in _dbContext.Pokemons where f.Group == 1 && f.Number == p.Number select 1)
                        .Any() select p).FirstOrDefaultAsync();
        }
        
        public virtual async Task<int> Insert(Pokemon pokemon)
        {
            _dbContext.Pokemons.Add(pokemon);
            return await _dbContext.SaveChangesAsync();
        }
        
        public virtual async Task<int> Update(int pkGroup, int pkNumber, Pokemon pokemon)
        {
//             return await _dbContext.Database.ExecuteSqlRawAsync(@"
// Update pokemons AS pk
// SET pk.[Group]=@p0,
//     pk.ApiSource='@p1',
//     pk.Name='@p2'
// WHERE pk.Number=@p3",
//                 pokemon.Group, pokemon.ApiSource, pokemon.Name, pokemon.Number);

            var pk = await _dbContext
                .Pokemons
                .Where(p=> p.Group == pkGroup && p.Number == pkNumber)
                .FirstOrDefaultAsync();
            if (pk.Equals(default)) { return default; }

            pk.Group = pokemon.Group;
            pk.Number = pokemon.Number;
            pk.Name = pokemon.Name;

            return await _dbContext.SaveChangesAsync();
        }
        
        public virtual async Task<List<Pokemon>> GetRange(int offset, int limit, int pkGroup=0)
        {
            IQueryable<Pokemon> flexPokemon = from f in _dbContext.Pokemons where f.Group == pkGroup && offset < f.Number && f.Number <= offset+limit select f;
            IQueryable<Pokemon> basePokemon = from b in _dbContext.Pokemons where b.Group == 0 && offset < b.Number && b.Number <= offset+limit select b;
            return await flexPokemon
                .Concat(
                    from p in basePokemon where !(
                        from f in _dbContext.Pokemons where f.Group == 1 && f.Number == p.Number select 1)
                        .Any() select p).ToListAsync();
        }
    }
}
