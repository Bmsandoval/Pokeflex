using App.Data;
using App.Services.PokeApiCo;
using App.Services.PokeApiGlitchMe;

namespace App.Services.PokeBase
{
    class PokemonServiceFactory
    {
        public static PokemonServiceFactoryProduct PokemonService() {
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

        private PokemonServiceFactory()
        {
        }
    }
}