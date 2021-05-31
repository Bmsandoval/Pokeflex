using System;
using System.Threading.Tasks;
using App.Data;
using App.Exceptions;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Engines
{
    public class PokeflexEngine
    {
        private readonly PokeflexService _svcPokeflexDb;
        private readonly ExtPokeApiServiceFactoryProduct _svcExtExtPokeApiApi;
        
        public PokeflexEngine(PokeflexService pokeflexService, ExtPokeApiServiceFactoryProduct extExtPokeApiApi)
        {
            _svcPokeflexDb = pokeflexService;
            _svcExtExtPokeApiApi = extExtPokeApiApi;
        }
        
        /// <summary>
        /// Selects a Pokemon in a given group with a given number.
        ///     If a pokemon in the specified group not found, defaults to base group
        ///     If a base pokemon not found, searches external Pokeapi for the pokemon
        ///         It will then insert the missing Pokemon into the base group
        /// </summary>
        /// <param name="num">pokemon's number</param>
        /// <param name="group">group number for a customized set of pokemon</param>
        /// <returns>Pokemon of the specified group&number, or base group if no pokemon found in specified group</returns>
        /// <exception cref="ExternalApiUnreachableException">Could not connect to provided Pokeapi</exception>
        /// <exception cref="DbUpdateException">Something went wrong while inserting</exception>
        public async Task<Pokemon> SelectPokemonInsertMissing(int num, int? group)
        {
            var pokemon = await _svcPokeflexDb.Select(num, group);
            if (pokemon != null)
                return pokemon;
            var ipokemon = _svcExtExtPokeApiApi.GetByNumber(num);
            if (ipokemon == null)
                throw new ExternalApiUnreachableException();
            pokemon = new Pokemon(ipokemon);
            await _svcPokeflexDb.Insert(pokemon);
            return pokemon;
        }
    }
}
