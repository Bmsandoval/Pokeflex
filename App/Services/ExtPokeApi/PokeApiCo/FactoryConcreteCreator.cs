using App.Services.ExtPokeApi.ApiFactoryBase;

namespace App.Services.ExtPokeApi.PokeApiCo
{
    class PokeapiCoServiceFactory : PokeflexServiceFactoryCreator
    {
        public override PokeflexServiceFactoryProduct GetPokemonService()
        {
            return new PokeapiCoService();
        }
    }
}