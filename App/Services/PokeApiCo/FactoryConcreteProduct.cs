using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using App.Services.PokeBase;

namespace App.Services.PokeApiCo
{
    class PokeapiCoService : PokemonServiceFactoryProduct
    {
        public readonly string apiSource = "pokeapi.co";
        
        public override Root GetByNumber(int id)
        {
            System.Console.WriteLine("requesting from " + apiSource);
            string url = "https://pokeapi.co/api/v2/pokemon/" + id;
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);

            // Get the response.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            string responseFromServer = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream is null)
                {
                    return null;
                }
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }

            // Close the response.
            response.Close();

            Root obj = JToken.Parse(responseFromServer).ToObject<Root>();
            if (obj is null)
            {
                return null;
            }

            // Root obj = objs[0];
            obj.apiSource = apiSource;
            
            return obj;
        }
    }
}