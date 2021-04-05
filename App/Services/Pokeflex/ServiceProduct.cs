using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using App.Data;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.Pokeflex
{
    public class PokeflexService
    {
        private PokeflexContext _dbContext;

        public PokeflexService(PokeflexContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public virtual Pokemon GetByNumber(int number)
        {
            var query = from pk in _dbContext.Pokemons
                where pk.Number.Equals(number)
                select pk;
            return query.FirstOrDefault();
        }
        
        public virtual Pokemon InsertPokemon(Pokemon pokemon)
        {
            _dbContext.Pokemons.Add(pokemon);
            _dbContext.SaveChanges();
            return pokemon;
        }
        
        public virtual Pokemon[] ListLocal()
        {
            List<Pokemon> pokemons = _dbContext.Pokemons.ToList();
        
            return pokemons.ToArray();
        }
        
        // public Flexmon InsertFlexmon(TargetModel.Pokemon pokemon)
        // {
        //     // int id = FlexmonTable.Count == 0 ? 0 : FlexmonTable.Keys.Max() + 1;
        //
        //     Flexmon flexmon = new Flexmon(pokemon);
        //
        //     if (FlexmonTable.ContainsKey(flexmon.number))
        //     {
        //         return null;
        //     }
        //     
        //     FlexmonTable.Add(flexmon.number, flexmon);
        //     return flexmon;
        // }
    }
}
