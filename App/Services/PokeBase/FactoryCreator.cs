namespace App.Services.PokeBase
{
    abstract class PokemonServiceFactoryCreator
    {
        public abstract PokemonServiceFactoryProduct GetPokemonService();
    }
}