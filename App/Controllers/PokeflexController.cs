using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Services;
using App.Services.PokeBase;
using App.Services.TargetModel;
using Microsoft.Extensions.Logging;
using FlexmonService = App.Services.Flexmon.FlexmonService;

namespace App.Controllers
{
    public class PokemonsController : Controller
    {
        private PokemonServiceFactoryProduct _svcExtPokemonApi;
        private FlexmonService _svcFlexmonDb;
     
        public PokemonsController(PokemonServiceFactoryProduct extPokemonApi, FlexmonService flexmonDb)
        {
            _svcExtPokemonApi = extPokemonApi;
            _svcFlexmonDb = flexmonDb;
        }
     
        // // GET: Pokemons
        // public async Task<IActionResult> Index()
        // {
        //     _pokemonService.Test();
        //     return Ok(await _pokemonService.List());
        // }
        
        //GET api/pokemon/id
        // [HttpGet]
        public  IActionResult Index()
        {
            Pokemon pokemon = _svcFlexmonDb.GetByNumber(200);
            if (pokemon is null)
            {
                Base basePokemon = _svcExtPokemonApi.GetByNumber(200);
                if(basePokemon is null)
                {
                    return StatusCode(400);
                }
         
                pokemon = new Pokemon(basePokemon);

                _svcFlexmonDb.InsertPokemon(pokemon);
            }
     
            return Ok(pokemon);
        }
        
        // //GET api/pokemon/id
        // [HttpGet("{id}")]
        // public ActionResult<Base> GetById(int id)
        // {
        //     Pokemon pokemon = PokeDBService.GetByNumber(id);
        //     if(pokemon is null)
        //     {
        //         Base uncachedPokemon = PokemonService.GetByNumber(id);
        //         if(uncachedPokemon is null)
        //         {
        //             return StatusCode(400);
        //         }
        //
        //         pokemon = PokeDBService.InsertPokemon(new Pokemon(uncachedPokemon));
        //         if(pokemon is null)
        //         {
        //             return StatusCode(400);
        //         }
        //     }
        //     
        //     return new ActionResult<Base>(pokemon);
        // }
        
        // //GET api/pokemon/local
        // [HttpGet("list/local")]
        // public ActionResult<IEnumerable<Pokemon>> ListLocal()
        // {
        //     var pokemon = PokeDBService.ListLocal();
        //     if(!pokemon.Any())
        //     {
        //         return StatusCode(400);
        //
        //     }
        //     return new ActionResult<IEnumerable<Pokemon>>(pokemon);
        // }
     
     //    // GET: Pokemons/Details/5
     //    public async Task<IActionResult> Details(int? id)
     //    {
     //        if (id == null)
     //        {
     //            return NotFound();
     //        }
     //
     //        var pokemon = await _context.Pokemons
     //            .Include(s => s.Enrollments)
     //            .ThenInclude(e => e.Course)
     //            .AsNoTracking()
     //            .FirstOrDefaultAsync(m => m.ID == id);
     //        if (pokemon == null)
     //        {
     //            return NotFound();
     //        }
     //
     //        return View(pokemon);
     //    }
     //
     //    // GET: Pokemons/Create
     //    public IActionResult Create()
     //    {
     //        return View();
     //    }
     //
     //    // POST: Pokemons/Create
     //    // To protect from overposting attacks, enable the specific properties you want to bind to.
     //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
     //    [HttpPost]
     //    // [ValidateAntiForgeryToken]
     //    public async Task<IActionResult> Create(
     //        [Bind("ID,LastName,FirstMidName,EnrollmentDate")] Pokemon pokemon)
     //    {
     //        try
     //        {
     //
     //
     //            if (ModelState.IsValid)
     //            {
     //                _context.Add(pokemon);
     //                await _context.SaveChangesAsync();
     //                return RedirectToAction(nameof(Index));
     //            }
     //        }
     //        catch (DbUpdateException exception)
     //        {
     //            ModelState.AddModelError("", "Unable to save changes. " +
     // "Try again, and if the problem persists " +
     // "see your system administrator." + exception);
     //        }
     //        return View(pokemon);
     //    }
     //
     //    // GET: Pokemons/Edit/5
     //    public async Task<IActionResult> Edit(int? id)
     //    {
     //        if (id == null)
     //        {
     //            return NotFound();
     //        }
     //
     //        var pokemon = await _context.Pokemons.FindAsync(id);
     //        if (pokemon == null)
     //        {
     //            return NotFound();
     //        }
     //        return View(pokemon);
     //    }
     //
     //    // POST: Pokemons/Edit/5
     //    // To protect from overposting attacks, enable the specific properties you want to bind to.
     //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
     //    [HttpPost, ActionName("Edit")]
     //    // [ValidateAntiForgeryToken]
     //    public async Task<IActionResult> EditPost(int? id)
     //    {
     //        if (id == null)
     //        {
     //            return NotFound();
     //        }
     //        var pokemonToUpdate = await _context.Pokemons.FirstOrDefaultAsync(s => s.ID == id);
     //        if (await TryUpdateModelAsync<Pokemon>(
     //            pokemonToUpdate,
     //            "",
     //            s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
     //        {
     //            try
     //            {
     //                await _context.SaveChangesAsync();
     //                return RedirectToAction(nameof(Index));
     //            }
     //            catch (DbUpdateException /* ex */)
     //            {
     //                //Log the error (uncomment ex variable name and write a log.)
     //                ModelState.AddModelError("", "Unable to save changes. " +
     //                    "Try again, and if the problem persists, " +
     //                    "see your system administrator.");
     //            }
     //        }
     //        return View(pokemonToUpdate);
     //    }
     //
     //    // GET: Pokemons/Delete/5
     //    public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
     //    {
     //        if (id == null)
     //        {
     //            return NotFound();
     //        }
     //
     //        var pokemon = await _context.Pokemons
     //            .AsNoTracking()
     //            .FirstOrDefaultAsync(m => m.ID == id);
     //        if (pokemon == null)
     //        {
     //            return NotFound();
     //        }
     //
     //        if (saveChangesError.GetValueOrDefault())
     //        {
     //            ViewData["ErrorMessage"] =
     //                "Delete failed. Try again, and if the problem persists " +
     //                "see your system administrator.";
     //        }
     //
     //        return View(pokemon);
     //    }
     //
     //    // POST: Pokemons/Delete/5
     //    [HttpPost, ActionName("Delete")]
     //    // [ValidateAntiForgeryToken]
     //    public async Task<IActionResult> DeleteConfirmed(int id)
     //    {
     //        var pokemon = await _context.Pokemons.FindAsync(id);
     //        if (pokemon == null)
     //        {
     //            return RedirectToAction(nameof(Index));
     //        }
     //
     //        try
     //        {
     //            _context.Pokemons.Remove(pokemon);
     //            await _context.SaveChangesAsync();
     //            return RedirectToAction(nameof(Index));
     //        }
     //        catch (DbUpdateException /* ex */)
     //        {
     //            //Log the error (uncomment ex variable name and write a log.)
     //            return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
     //        }
     //    }
    }
}
