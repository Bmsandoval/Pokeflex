using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    [DataContract][Serializable][Index(nameof(Group), nameof(Number))]
    public class Pokemon : IPokemon
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public int Group { get; set; }
        [DataMember] public string ApiSource { get; set; }
        [DataMember] public int Number { get; set; }
        [DataMember] public string Name { get; set; }
        
        public Pokemon() { Group = 0; }

        public Pokemon(int group=0) { Group = group; }
        
        public Pokemon(IPokemon ipokemon, int group=0)
        {
            Group = group;
            ApiSource = ipokemon.ApiSource;
            Number = ipokemon.Number;
            Name = ipokemon.Name;
        }

        public override int GetHashCode()
        {
            return Group.GetHashCode() * 17 +
                   Number.GetHashCode() * 17 +
                   ApiSource.GetHashCode() * 17 +
                   Id.GetHashCode() * 17 +
                   Name.GetHashCode() * 17;
        }

        #nullable enable
        public override bool Equals(object? obj)
        {
            if (!(obj is Pokemon testmon)) { return false; }

            return Id == testmon.Id &&
                   Group == testmon.Group &&
                   Number == testmon.Number &&
                   ApiSource == testmon.ApiSource &&
                   Name == testmon.Name;
        }
        #nullable disable
    }
}
