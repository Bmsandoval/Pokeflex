using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class FlexmonService
    {
        // private readonly FlexmonContext _context;
        // public FlexmonService(FlexmonContext databaseContext) {
        //     this._context = databaseContext;
        // }
        // public virtual async Task<List<Pokemon>> List()
        // {
        //     return await _context.Pokemons.ToListAsync();
        // }
        //
        // public virtual string Test()
        // {
        //     var query = from s in _context.Pokemons
        //         where s.Id.Equals(1)
        //         select s;
        //     // var result = query.ToList();
        //     Console.WriteLine(query.ToQueryString());
        //     return query.ToString();
        // }
    }
}