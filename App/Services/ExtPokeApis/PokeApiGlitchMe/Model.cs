using System.Collections.Generic;
using App.Models;
using Newtonsoft.Json;
using App.Services.ExtPokeApis.ApiFactoryBase;

namespace App.Services.ExtPokeApis.PokeApiGlitchMe
{
    public class Abilities
    {
        public List<string> normal { get; set; }
        public List<string> hidden { get; set; }
    }

    public class Family
    {
        public int id { get; set; }
        public int evolutionStage { get; set; }
        public List<string> evolutionLine { get; set; }
    }

    public class Glitchmon : Basemon
    {
        public override string ApiSource { get; set; }
        public override int Number { get; set; }
        public override string Name { get; set; }
        public string species { get; set; }
        public List<string> types { get; set; }
        public Abilities abilities { get; set; }
        public List<string> eggGroups { get; set; }
        public List<double> gender { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public Family family { get; set; }
        public bool starter { get; set; }
        public bool legendary { get; set; }
        public bool mythical { get; set; }
        public bool ultraBeast { get; set; }
        public bool mega { get; set; }
        public int gen { get; set; }
        public string sprite { get; set; }
        public string description { get; set; }
    }
}