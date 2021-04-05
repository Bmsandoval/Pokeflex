using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.ExtPokeApis.PokeApiCo
{
    class PokeapiCoServiceFactory : IExtPokeApiServiceFactoryCreator
    {
        public ExtPokeApiServiceFactoryProduct GetPokemonService()
        {
            return new PokeapiCoService();
        }
    }
}