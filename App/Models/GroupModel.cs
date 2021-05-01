using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using App.Data;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    public class Group
    {
        public int Id { get; set; }

        public IList<UserGroup> UserGroups { get; set; }
        
        public IList<Pokemon> Pokemons { get; set; }
    }
}
