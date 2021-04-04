using App.Data;
using App.Services.ExtPokeApi.PokeApiCo;
using App.Services.ExtPokeApi.PokeApiGlitchMe;

namespace App.Services.ExtPokeApi.ApiFactoryBase
{
    class PokeflexServiceFactory
    {
        public static PokeflexServiceFactoryProduct PokemonService() {
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

        private PokeflexServiceFactory()
        {
        }
    }
}