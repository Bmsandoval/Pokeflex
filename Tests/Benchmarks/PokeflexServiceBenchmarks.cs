using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;

namespace Benchmarks
{
    // [RPlotExporter]
    [ShortRunJob] // for quicker, but less accurate results
    public class PokeflexServiceBenchmarks 
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Params(1, 2)] public int Groups;
        [Params(50, 100)] public int Numbers;
        private List<Pokemon> pokemons;

        [GlobalSetup]
        public void Setup()
        {
            pokemons = new List<Pokemon>();
            int id = 0;
            Random randGen = new Random();
            for (int g = 0; g <= Groups; g++)
            {
                int rand = randGen.Next(0,7);
                for (int n=1; n <= Numbers; n++)
                {
                    if (n % 7 == 0) { rand= randGen.Next(0,7); }
                    // maintain base group, but skip every seventh of the flex group
                    if (g != 0 && rand != 0)
                    {
                        id++;
                        pokemons.Add(new Pokemon
                        {
                            Group = g,
                            Id = id,
                            Number = n,
                            Name = "salt supreme"
                        });
                    }
                }
            }
        }

        [BenchmarkCategory("Select"), Benchmark(Baseline=true)]
        public async Task<Pokemon> Baseline()
        {
            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            return await pokeflexService.Select(-1);
        }

        [BenchmarkCategory("Select"), Benchmark]
        public async Task<Pokemon> BenchmarkSelect()
        {
            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            return await pokeflexService.Select(42);
        }
    }
}
