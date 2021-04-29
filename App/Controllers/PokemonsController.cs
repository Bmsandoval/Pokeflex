using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("pokemon")]
    public class PokemonsController : Controller
    {
        private readonly ExtPokeApiServiceFactoryProduct _svcExtExtPokeApiApi;
        private readonly PokeflexService _svcPokeflexDb;
        private readonly GroupService _svcGroupDb;
        public PokemonsController(ExtPokeApiServiceFactoryProduct extExtPokeApiApi, PokeflexService pokeflexService, GroupService groupService)
        {
            _svcExtExtPokeApiApi = extExtPokeApiApi ?? throw new ArgumentNullException(nameof(extExtPokeApiApi));
            _svcPokeflexDb = pokeflexService ?? throw new ArgumentNullException(nameof(pokeflexService));
            _svcGroupDb = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpGet] public async Task<IActionResult> List([FromQuery]int offset=0, [FromQuery]int limit=5, [FromQuery]int? group=null) 
        {
            var pokemons = await _svcPokeflexDb.GetRange(offset, limit, group);
            if (pokemons.Count == limit) return Ok(pokemons);
            var numbers = (from pk in pokemons select pk.Number).ToHashSet();
            for(var num=offset+1;num<=offset+limit;num++) {
                if (numbers.Contains(num)) continue;
                var ipokemon = _svcExtExtPokeApiApi.GetByNumber(num);
                if (ipokemon == null) return NoContent();
                var pokemon = new Pokemon(ipokemon);
                await _svcPokeflexDb.Insert(pokemon);
                pokemons.Add(pokemon);
            }
            return Ok(pokemons);
        }
        
        [Route("{num:int}")]
        [HttpGet] public async Task<IActionResult> Select(int num, [FromQuery]int? group=null)
        {
            var pokemon = await _svcPokeflexDb.Select(num, group);
            if (pokemon != null) return Ok(pokemon);
            var ipokemon = _svcExtExtPokeApiApi.GetByNumber(num);
            if (ipokemon == null) return NoContent();
            pokemon = new Pokemon(ipokemon);
            int count;
            try {
                count = await _svcPokeflexDb.Insert(pokemon);
            } catch (DbUpdateException e) {
                return BadRequest("specified group doesn't exist");
            }
            if (count < 1) return BadRequest();
            return Ok(pokemon);
        }
        
        [HttpPost] public async Task<IActionResult> Insert([FromBody]Pokemon pokemon)
        {
            if (pokemon.GroupId is null) return BadRequest("must include a group id");
            int count;
            try
            {
                count = await _svcPokeflexDb.Insert(pokemon);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            if (count < 1) return BadRequest();
            return Ok(pokemon);
        }
        
        [HttpPut] public async Task<IActionResult> Update([FromQuery]int pkGroup, [FromQuery]int pkNumber, [FromBody]Pokemon pokemon)
        {
            if (pkGroup < 1) return Conflict();
            var count = await _svcPokeflexDb.Update(pkGroup, pkNumber, pokemon);
            if (count < 1) return NotFound("Flexmon with number not found");
            return Ok(pokemon);
        }
    }
}
