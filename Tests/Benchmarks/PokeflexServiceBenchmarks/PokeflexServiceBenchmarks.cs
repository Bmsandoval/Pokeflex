using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Benchmarks.PokeflexServiceBenchmarks
{
    // [RPlotExporter]
    [ShortRunJob] // for quicker, but less accurate results
    public class PokeflexServiceSelectBenchmarks 
    {
        [Params(1, 2)] public int Groups;
        [Params(50, 100)] public int Numbers;

        [GlobalSetup]
        public void Setup()
        {
            List<Pokemon> pokemons = new();
            int id = 0;
            for (int g = 0; g <= Groups; g++)
            {
                for (int n=1; n <= Numbers; n++)
                {
                    id++;
                    pokemons.Add(new Pokemon {
                        Group = g,
                        Id = id,
                        Number = n,
                        Name = $"pokemon_{g}_{n}"
                    });
                }
            }
            
            var contextType =
                Environment.GetEnvironmentVariable("PokeflexBenchmarkDbType", EnvironmentVariableTarget.Process)
                ?? "";
            
            DbContextFactory.InitDbContext(contextType, pokemons.ToArray());
        }

        
        [BenchmarkCategory("Select"), Benchmark(Baseline=true)]
        public async Task<Pokemon> BaselineSelect()
        {
            var service = new PokeflexService(DbContextFactory.DbContext.PokeflexContext);
            return await service.Select(-1);
        }

        
        [BenchmarkCategory("Select"), Benchmark]
        public async Task<Pokemon> BenchmarkSelect()
        {
            var service = new PokeflexService(DbContextFactory.DbContext.PokeflexContext);
            return await service.Select(42);
        }
    }
    
    
    // [ShortRunJob] // for quicker, but less accurate results
    // public class PokeflexServiceAnotherSelectBenchmarks 
    // {
    //     [Params(1, 2)] public int Groups;
    //     [Params(50, 100)] public int Numbers;
    //
    //     [GlobalSetup]
    //     public void Setup()
    //     {
    //         DbContext.Instance.InitDbContext("");
    //         int id = 0;
    //         for (int g = 0; g <= Groups; g++)
    //         {
    //             for (int n=1; n <= Numbers; n++)
    //             {
    //                 id++;
    //                 DbContext.Instance.Context.Pokemons.Add(new Pokemon {
    //                     Group = g,
    //                     Id = id,
    //                     Number = n,
    //                     Name = $"pokemon_{g}_{n}"
    //                 });
    //             }
    //         }
    //         
    //         DbContext.Instance.Context.SaveChanges();
    //     }
    //     
    //     
    //     [BenchmarkCategory("AnotherSelect"), Benchmark(Baseline=true)]
    //     public async Task<Pokemon> BaselineAnotherSelect()
    //     {
    //         var service = new PokeflexService(DbContext.Instance.Context);
    //         return await service.Select(-1);
    //     }
    //
    //     
    //     [BenchmarkCategory("AnotherSelect"), Benchmark]
    //     public async Task<Pokemon> BenchmarkAnotherSelect()
    //     {
    //         var service = new PokeflexService(DbContext.Instance.Context);
    //         return await service.Select(42);
    //     }
    // }
}
