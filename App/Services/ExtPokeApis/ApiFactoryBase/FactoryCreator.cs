namespace App.Services.ExtPokeApis.ApiFactoryBase
{
    interface IExtPokeApiServiceFactoryCreator
    {
        public ExtPokeApiServiceFactoryProduct GetPokemonService();
    }
}