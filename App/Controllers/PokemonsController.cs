using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;

namespace App.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly ExtPokeApiServiceFactoryProduct _svcExtExtPokeApiApi;
        private readonly PokeflexService _svcPokeflexDb;
     
        public PokemonsController(ExtPokeApiServiceFactoryProduct extExtPokeApiApi, PokeflexService pokeflexDb)
        {
            _svcExtExtPokeApiApi = extExtPokeApiApi ?? throw new ArgumentNullException(nameof(extExtPokeApiApi));
            _svcPokeflexDb = pokeflexDb ?? throw new ArgumentNullException(nameof(pokeflexDb));
        }

        [HttpGet][Route("pokemon/")]
        public async Task<IActionResult> List([FromQuery]int offset=0, [FromQuery]int limit=5, [FromQuery]int group=0)
        {
            List<Pokemon> pokemons = await _svcPokeflexDb.GetRange(offset, limit, group);
            if (pokemons.Count == limit) { return Ok(pokemons); }
            HashSet<int> numbers = (from pk in pokemons select pk.Number).ToHashSet();
            for(int num=offset+1;num<=offset+limit;num++)
            {
                if (! numbers.Contains(num))
                {
                    IPokemon querymon = _svcExtExtPokeApiApi.GetByNumber(num);
                    if (querymon == null) { return NoContent(); }
                    
                    Pokemon pokemon = new Pokemon(querymon);
                    await _svcPokeflexDb.Insert(pokemon);
                    pokemons.Add(pokemon);
                }
            }
            return Ok(pokemons);
        }
        
        [HttpGet][Route("pokemon/{num}")]
        public async Task<IActionResult> Select(int num, [FromQuery]int group=0)
        {
            Pokemon pokemon = await _svcPokeflexDb.Select(num, group);
            if (pokemon == null)
            {
                IPokemon querymon = _svcExtExtPokeApiApi.GetByNumber(num);
                if (querymon == null) { return NoContent(); }
                
                pokemon = new Pokemon(querymon);
                await _svcPokeflexDb.Insert(pokemon);
            }
            return Ok(pokemon);
        }
        
        [HttpPost][Route("pokemon/")]
        public async Task<IActionResult> Insert([FromBody]Pokemon pokemon)
        {
            int count = await _svcPokeflexDb.Insert(pokemon);
            if (count < 1) { BadRequest(); }
            return Ok(pokemon);
        }
        
        [HttpPut][Route("pokemon/")]
        public async Task<IActionResult> Update([FromQuery]int pkGroup, [FromQuery]int pkNumber, [FromBody]Pokemon pokemon)
        {
            if (pkGroup < 1) { return Conflict(); }
            int count = await _svcPokeflexDb.Update(pkGroup, pkNumber, pokemon);
            if (count < 1) { return NotFound("Flexmon with number not found"); }
            return Ok(pokemon);
        }
    }
}
