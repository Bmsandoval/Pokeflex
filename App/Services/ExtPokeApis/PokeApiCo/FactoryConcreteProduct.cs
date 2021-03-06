using System;
using System.IO;
using System.Net;
using App.Models;
using Newtonsoft.Json.Linq;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Shared;

namespace App.Services.ExtPokeApis.PokeApiCo
{
    public class PokeapiCoService : ExtPokeApiServiceFactoryProduct
    {
        public readonly string apiSource = "pokeapi.co";
        
        public override IPokemon GetByNumber(int id)
        {
            System.Console.WriteLine("requesting from " + apiSource);
            string url = "https://pokeapi.co/api/v2/pokemon/" + id;
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            // Get the response.
            WebResponse response = SendRequest(request);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            string responseFromServer = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream is null)
                {
                    return default;
                }
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }

            // Close the response.
            response.Close();
            
            var obj = StreamHelpers.FromJsonString<Pocomon>(responseFromServer);

            obj.ApiSource = apiSource;
            
            return obj;
        }
    }
}