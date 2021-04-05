// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using App.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    // [Serializable]
    // public class Pokemon
    // {
    //     public virtual int Number { get; set; }
    //     public virtual string ApiSource { get; set; }
    //     public virtual string Name { get; set; }
    //
    //     public Pokemon() {}
    //
    //     public Pokemon(Pokemon copy)
    //     {
    //         Number = copy.Number;
    //         ApiSource = copy.ApiSource;
    //         Name = copy.Name;
    //     }
    //     public MemoryStream ToJsonStream<T>() where T: Pokemon
    //     {
    //         return StreamHelpers.SerializeToStream(
    //             JsonConvert.SerializeObject(
    //                 (T)this));
    //     }
    //
    //     public static T FromJsonStream<T>(MemoryStream stream)
    //     {
    //         return JToken.Parse(
    //             StreamHelpers.DeserializeFromStream(stream)
    //             ).ToObject<T>();
    //     }
    //     
    //     public override int GetHashCode()
    //     {
    //         return Number.GetHashCode() * 17 +
    //                ApiSource.GetHashCode() * 17 +
    //                Name.GetHashCode() * 17;
    //     }
    //
    //     #nullable enable
    //     public override bool Equals(object? obj)
    //     {
    //         if (!(obj is Pokemon testmon)) { return false; }
    //
    //         return Number == testmon.Number &&
    //                ApiSource == testmon.ApiSource &&
    //                Name == testmon.Name;
    //     }
    //     #nullable disable
    // }
}