using App.Services.PokeBase;

namespace App.Services.Flexmon
{
    public sealed class Flexmon : Base
    {
        public int id { get; set; }
        public string source { get; set; }
        public override int number { get; set; }
        public override string apiSource { get; set; }
        public override string name { get; set; }

        public Flexmon() { }

        public Flexmon(TargetModel.Pokemon copy) : base(copy)
        {
            source = "FlexmonTable";
        }
    }
}