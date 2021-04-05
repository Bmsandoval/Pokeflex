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
        public int GetHashCode();

        #nullable enable
        public bool Equals(object? obj);
        #nullable disable
        
        public static MemoryStream ToJsonStream<T>(T obj) where T: IPokemon
        {
            return StreamHelpers.SerializeToStream(ToJsonString(obj));
        }

        public static string ToJsonString<T>(T obj) where T: IPokemon
        {
            return JsonConvert.SerializeObject(obj, new BinaryConverter());
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
