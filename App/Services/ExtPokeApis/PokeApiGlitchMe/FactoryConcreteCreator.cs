using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.ExtPokeApis.PokeApiGlitchMe
{
    class PokeGlitchServiceFactory : ExtPokeApiServiceFactoryCreator
    {
        public override ExtPokeApiServiceFactoryProduct GetPokemonService()
        {
            return new PokeApiGlitchService();
        }
    }
}