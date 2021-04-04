using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Services.TargetModel;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class PokemonService
    {
        private readonly PokeflexContext _context;
        public PokemonService(PokeflexContext databaseContext) {
            this._context = databaseContext;
        }
        public virtual async Task<List<Pokemon>> List()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public virtual string Test()
        {
            var query = from s in _context.Pokemons
                where s.number.Equals(1)
                select s;
            // var result = query.ToList();
            Console.WriteLine(query.ToQueryString());
            return query.ToString();
        }
    }
}