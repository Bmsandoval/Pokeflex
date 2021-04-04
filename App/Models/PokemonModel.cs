using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    [Serializable]
    public class Pokemon : IPokemon
    {
        public string Source { get; set; }
        public string ApiSource { get; set; }
        public int Number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public Pokemon()
        {
            Source = "PokemonTable";
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode() * 17 +
                   ApiSource.GetHashCode() * 17 +
                   Id.GetHashCode() * 17 +
                   Name.GetHashCode() * 17;
        }

        #nullable enable
        public override bool Equals(object? obj)
        {
            if (!(obj is Pokemon testmon)) { return false; }

            return Id == testmon.Id &&
                   Source == testmon.Source &&
                   Number == testmon.Number &&
                   ApiSource == testmon.ApiSource &&
                   Name == testmon.Name;
        }
        #nullable disable
        
        public static explicit operator Pokemon(Basemon basemon)
        {
            return new ()
            {
                ApiSource = basemon.ApiSource,
                Name = basemon.Name,
                Number = basemon.Number
            };
        }
    }
}
