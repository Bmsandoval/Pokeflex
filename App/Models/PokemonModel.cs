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
    [DataContract]
    [Serializable]
    public class Pokemon : IPokemon
    {
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public virtual string ApiSource { get; set; }
        [DataMember]
        public virtual int Number { get; set; }
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual string Name { get; set; }

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
    }
}
