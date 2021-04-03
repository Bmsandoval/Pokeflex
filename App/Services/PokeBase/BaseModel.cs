// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Services.PokeBase
{
    public class Base
    {
        public virtual int number { get; set; }
        public virtual string apiSource { get; set; }
        public virtual string name { get; set; }

        public Base() {}

        public Base(Base copy)
        {
            number = copy.number;
            apiSource = copy.apiSource;
            name = copy.name;
        }
    }
}