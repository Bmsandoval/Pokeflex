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
        public virtual string ApiSource { get => ApiSource; set => ApiSource = value; }
        public virtual int Number { get => Number; set => Number = value; }
        public virtual string Name { get => Name; set => Name = value; }

        public virtual int GetHashCode()
        {
            return Number.GetHashCode() * 17 +
                   ApiSource.GetHashCode() * 17 +
                   Name.GetHashCode() * 17;
        }

        #nullable enable
        public virtual bool Equals(object? obj)
        {
            if (!(obj is Pokemon testmon)) { return false; }

            return Number == testmon.Number &&
                   ApiSource == testmon.ApiSource &&
                   Name == testmon.Name;
        }
        #nullable disable
    }
}
