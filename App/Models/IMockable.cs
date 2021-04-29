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
    public interface IMockable
    {
        public static T NewMock<T>() where T : IMockable
        {
            return NewMock<T>();
        }
    }
}
