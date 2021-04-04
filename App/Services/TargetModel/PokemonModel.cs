using App.Services.PokeBase;

namespace App.Services.TargetModel
{
    public class Pokemon : Base
    {
        public int id { get; set; }
        public string source { get; set; }
        public override int number { get; set; }
        public override string apiSource { get; set; }
        public override string name { get; set; }

        public Pokemon() { }

        public Pokemon(Base copy) : base(copy)
        {
            source = "PokemonTable";
        }
    }
}
