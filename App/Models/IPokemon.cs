using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        
        public Stream ToJsonStream<T>() where T: IPokemon
        {
            return StreamHelpers.SerializeToStream(
                JsonConvert.SerializeObject(
                    (T)this, new BinaryConverter()));
        }

        public static T FromJsonStream<T>(MemoryStream stream)
        {
            return FromJsonString<T>(StreamHelpers.DeserializeFromStream(stream));
        }

        public static T FromJsonString<T>(string json)
        {
            return JToken.Parse(json).ToObject<T>();
        }
    }
}
