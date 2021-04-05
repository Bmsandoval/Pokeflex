using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.ExtPokeApis.PokeApiGlitchMe
{
    class PokeGlitchServiceFactory : IExtPokeApiServiceFactoryCreator
    {
        public ExtPokeApiServiceFactoryProduct GetPokemonService()
        {
            return new PokeApiGlitchService();
        }
    }
}