using App.Services.ExtPokeApi.ApiFactoryBase;

namespace App.Services.ExtPokeApi.PokeApiGlitchMe
{
    class PokeGlitchServiceFactory : PokeflexServiceFactoryCreator
    {
        public override PokeflexServiceFactoryProduct GetPokemonService()
        {
            return new PokeflexGlitchService();
        }
    }
}