using System;
using System.IO;
using System.Net;
using App.Models;
using Newtonsoft.Json.Linq;
using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.ExtPokeApis.PokeApiGlitchMe
{
    class PokeApiGlitchService : ExtPokeApiServiceFactoryProduct
    {
        public readonly string apiSource = "pokeapi.glitch.me";
        
        public override Basemon GetByNumber(int id)
        {
            System.Console.WriteLine("requesting from " + apiSource);
            string url = "https://pokeapi.glitch.me/v1/pokemon/" + id;
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            System.Console.WriteLine(request.Method);
            System.Console.WriteLine(request.Headers);

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

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

            Glitchmon obj = JToken.Parse(responseFromServer).ToObject<Glitchmon>();
            if (obj is null)
            {
                return null;
            }

            // Root obj = objs[0];
            obj.ApiSource = apiSource;
            
            return obj;
        }
    }
}