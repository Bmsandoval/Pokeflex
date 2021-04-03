using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace App.Services.PokeBase
{
    public abstract class PokemonServiceFactoryProduct
    {
        public abstract Base GetByNumber(int id);
    }
}