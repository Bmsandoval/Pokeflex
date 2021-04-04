using App.Services.ExtPokeApi.ApiFactoryBase;

namespace App.Services.Pokeflex
{
    public class Pokemon : Basemon
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public override int Number { get; set; }
        public override string ApiSource { get; set; }
        public override string Name { get; set; }

        public Pokemon() { }

        public Pokemon(Basemon copy) : base(copy)
        {
            Source = "PokemonTable";
        }

        public override bool Equals(object? obj)
        {
            Pokemon testmon = obj as Pokemon;
            if (testmon == null)
            {
                return false;
            }

            return Id == testmon.Id &&
                   Source == testmon.Source &&
                   Number == testmon.Number &&
                   ApiSource == testmon.ApiSource &&
                   Name == testmon.Name;
        }
    }
}
