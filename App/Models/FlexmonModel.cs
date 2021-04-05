using System.IO;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    public sealed class Flexmon : IPokemon
    {
        public string Source { get; set; }
        public string ApiSource { get; set; }
        public int Number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public Flexmon()
        {
            Source = "FlexmonTable";
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
            if (!(obj is Flexmon testmon)) { return false; }

            return Id == testmon.Id &&
                   Source == testmon.Source &&
                   Number == testmon.Number &&
                   ApiSource == testmon.ApiSource &&
                   Name == testmon.Name;
        }
        #nullable disable
    }
}