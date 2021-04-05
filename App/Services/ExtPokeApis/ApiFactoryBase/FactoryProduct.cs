using System;
using System.IO;
using System.Net;
using App.Data;
using App.Models;
using App.Shared;
using Newtonsoft.Json.Linq;

namespace App.Services.ExtPokeApis.ApiFactoryBase
{
    public abstract class ExtPokeApiServiceFactoryProduct
    {
        public abstract Pokemon GetByNumber(int id);

        public virtual WebResponse SendRequest(WebRequest _request)
        {
            return _request.GetResponse();
        }
    }
}