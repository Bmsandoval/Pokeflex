// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Services.ExtPokeApi.ApiFactoryBase
{
    public class Basemon
    {
        public virtual int Number { get; set; }
        public virtual string ApiSource { get; set; }
        public virtual string Name { get; set; }

        public Basemon() {}

        public Basemon(Basemon copy)
        {
            Number = copy.Number;
            ApiSource = copy.ApiSource;
            Name = copy.Name;
        }
    }
}