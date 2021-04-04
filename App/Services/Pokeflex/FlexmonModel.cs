using App.Services.ExtPokeApi.ApiFactoryBase;

namespace App.Services.Pokeflex
{
    public sealed class Flexmon : Basemon
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public override int Number { get; set; }
        public override string ApiSource { get; set; }
        public override string Name { get; set; }

        public Flexmon() { }

        public Flexmon(Pokemon copy) : base(copy)
        {
            Source = "FlexmonTable";
        }
        
        public override bool Equals(object? obj)
        {
            Flexmon testmon = obj as Flexmon;
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