using System;
using System.IO;
using System.Net;
using App.Data;
using Newtonsoft.Json.Linq;

namespace App.Services.ExtPokeApi.ApiFactoryBase
{
    public abstract class PokeflexServiceFactoryProduct
    {
        public abstract Basemon GetByNumber(int id);
    }
}