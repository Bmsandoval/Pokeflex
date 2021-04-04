namespace App.Services.ExtPokeApis.ApiFactoryBase
{
    abstract class ExtPokeApiServiceFactoryCreator
    {
        public abstract ExtPokeApiServiceFactoryProduct GetPokemonService();
    }
}