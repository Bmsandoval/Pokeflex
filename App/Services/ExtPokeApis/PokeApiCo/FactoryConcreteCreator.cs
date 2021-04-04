using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.ExtPokeApis.PokeApiCo
{
    class PokeapiCoServiceFactory : ExtPokeApiServiceFactoryCreator
    {
        public override ExtPokeApiServiceFactoryProduct GetPokemonService()
        {
            return new PokeapiCoService();
        }
    }
}