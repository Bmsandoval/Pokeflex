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
        private readonly ExtPokeApiServiceFactoryProduct _svcExtExtPokeApiApi;
        private readonly PokeflexService _svcPokeflexDb;
     
        public PokeflexController(ExtPokeApiServiceFactoryProduct extExtPokeApiApi, PokeflexService pokeflexDb)
        {
            _svcExtExtPokeApiApi = extExtPokeApiApi ?? throw new ArgumentNullException(nameof(extExtPokeApiApi));
            _svcPokeflexDb = pokeflexDb ?? throw new ArgumentNullException(nameof(pokeflexDb));
        }
     
        [Route("pokemons/")]
        public async Task<IActionResult> List([FromQuery]int offset=0, [FromQuery]int limit=1)
        {
            List<Pokemon> pokemons = await _svcPokeflexDb.GetRange(offset, limit);
            if (pokemons.Count == limit) { return Ok(pokemons); }
            HashSet<int> numbers = (from pk in pokemons select pk.Number).ToHashSet();
            for(int num=offset+1;num<=offset+limit;num++)
            {
                if (! numbers.Contains(num))
                {
                    IPokemon querymon = _svcExtExtPokeApiApi.GetByNumber(num);
                    if (querymon == null) { return NoContent(); }
                    
                    Pokemon pokemon = new Pokemon(querymon);
                    _svcPokeflexDb.InsertPokemon(pokemon);
                    pokemons.Add(pokemon);
                }
            }
            return Ok(pokemons);
        }
        
        [Route("pokemons/{num}")]
        public  IActionResult Index(string num)
        {
            int _num;
            if (! int.TryParse(num, out _num)) { return BadRequest(); }
            Pokemon pokemon = _svcPokeflexDb.GetByNumber(_num);
            if (pokemon == null)
            {
                IPokemon querymon = _svcExtExtPokeApiApi.GetByNumber(_num);
                if (querymon == null) { return NoContent(); }
                
                pokemon = new Pokemon(querymon);
                _svcPokeflexDb.InsertPokemon(pokemon);
            }

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
