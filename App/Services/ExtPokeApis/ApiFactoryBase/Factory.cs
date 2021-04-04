using App.Data;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Services.ExtPokeApis.PokeApiGlitchMe;

namespace App.Services.ExtPokeApis.ApiFactoryBase
{
    class ExtPokeApiServiceFactory
    {
        public static ExtPokeApiServiceFactoryProduct PokemonService() {
            var service = "pokeapi.co";
            switch (service)
            {
                case "glitch.me":
                    return (new PokeGlitchServiceFactory()).GetPokemonService();
                case "pokeapi.co":
                    return (new PokeapiCoServiceFactory()).GetPokemonService();
                default:
                    return null;
            }
        }

        private ExtPokeApiServiceFactory()
        {
        }
    }
}