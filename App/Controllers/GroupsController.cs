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
    [Route("group")]
    public class GroupsController : Controller
    {
        private readonly ExtPokeApiServiceFactoryProduct _svcExtExtPokeApiApi;
        private readonly GroupService _svcGroupDb;
        public GroupsController(GroupService svcGroupDb) => _svcGroupDb = svcGroupDb ?? throw new ArgumentNullException(nameof(svcGroupDb));

        [HttpPost] public async Task<IActionResult> Insert()
        {
            var group = new Group();
            var count = await _svcGroupDb.Insert(group);
            if (count < 1) BadRequest();
            return Ok(group);
        }
        
        [HttpGet] public async Task<IActionResult> List() => Ok(await _svcGroupDb.List());
    }
}
