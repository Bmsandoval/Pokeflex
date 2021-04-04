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
    public interface IPokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GetHashCode();

        #nullable enable
        public bool Equals(object? obj);
        #nullable disable
        
        public MemoryStream ToJsonStream<T>() where T: IPokemon
        {
            return StreamHelpers.SerializeToStream(
                JsonConvert.SerializeObject(
                    (T)this));
        }

        public static T FromJsonStream<T>(MemoryStream stream)
        {
            return JToken.Parse(
                (string)StreamHelpers.DeserializeFromStream(stream)
                ).ToObject<T>();
        }
    }
}
