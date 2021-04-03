using App.Services.PokeBase;

namespace App.Services.PokeApiGlitchMe
{
    class PokeGlitchServiceFactory : PokemonServiceFactoryCreator
    {
        public override PokemonServiceFactoryProduct GetPokemonService()
        {
            return new PokemonGlitchService();
        }
    }
}