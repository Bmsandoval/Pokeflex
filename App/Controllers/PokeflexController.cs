using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using App.Services;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;
using App.Shared;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    public class PokeflexController : Controller
    {
        private ExtPokeApiServiceFactoryProduct _svcExtExtPokeApiApi;
        private PokeflexService _svcPokeflexDb;
     
        public PokeflexController(ExtPokeApiServiceFactoryProduct extExtPokeApiApi, PokeflexService pokeflexDb)
        {
            _svcExtExtPokeApiApi = extExtPokeApiApi;
            _svcPokeflexDb = pokeflexDb;
        }
     
        // // GET: Pokemons
        // public async Task<IActionResult> Index()
        // {
        //     _pokemonService.Test();
        //     return Ok(await _pokemonService.List());
        // }
        [Route("pokemons/local")]
        public IActionResult List()
        {
            return Ok(_svcPokeflexDb.ListLocal());
        }
        
        //GET api/pokemon/id
        [Route("pokemons/{id}")]
        public  IActionResult Index(string id)
        {
            int _id;
            if (! int.TryParse(id, out _id)) { return BadRequest(default); }

            Pokemon pokemon = _svcPokeflexDb.GetByNumber(_id);
            if (pokemon!=null) { return Ok(pokemon); }
            
            pokemon = _svcExtExtPokeApiApi.GetByNumber(_id);
            if(pokemon.Equals(default(Pokemon))) { return StatusCode(400); }
            
            pokemon = _svcPokeflexDb.InsertPokemon(pokemon);
            
            return Ok(StreamHelpers.ToJsonString(pokemon));
        }
        
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
