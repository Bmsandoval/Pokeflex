using App.Services.PokeBase;

namespace App.Services.PokeApiCo
{
    class PokeapiCoServiceFactory : PokemonServiceFactoryCreator
    {
        public override PokemonServiceFactoryProduct GetPokemonService()
        {
            return new PokeapiCoService();
        }
    }
}