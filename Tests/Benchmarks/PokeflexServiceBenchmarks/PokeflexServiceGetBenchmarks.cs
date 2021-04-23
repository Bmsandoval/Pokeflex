using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Xunit.Sdk;

namespace Benchmarks.PokeflexServiceBenchmarks
{
    [CategoriesColumn]
    public class PokeflexServiceGetBenchmarks
    {
        [Params(1, 10)] public int Groups;
        [Params(100, 1000)] public int Numbers;

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

        [Benchmark(Baseline = true)]
        public async Task<Pokemon> GetOneLinqBaseline()
        {
            var pokeflexContext=DbContextFactory.DbContext.PokeflexContext;
            string sql = @"
SELECT *
FROM [Pokemons] AS [p]";
            var pokemon = await pokeflexContext.Pokemons.FromSqlRaw(sql).FirstOrDefaultAsync();
            return pokemon;
        }

        
        [Benchmark]
        [BenchmarkCategory("Linq")]
        public async Task<Pokemon> GetUnionWhereNotExistsLinq()
        {
            Random r = new Random();
            var service = new PokeflexService(DbContextFactory.DbContext.PokeflexContext);
            var pokemon = await service.Select(r.Next(0,Numbers), r.Next(1, Groups));
            return pokemon;
        }
        
        [Benchmark]
        [BenchmarkCategory("Raw")]
        public Pokemon GetUnionWhereNotExistsCteRaw()
        {
            Random r = new Random();
            var pokeflexContext=DbContextFactory.DbContext.PokeflexContext;
            string sql = @"
WITH fnums ([Number]) AS ( 
    SELECT [Number]
    FROM pokemons
    WHERE [Group] = @p0 AND [Number] = @p1
)
SELECT [Id], [ApiSource], [Group], [Name], [Number]
FROM pokemons
WHERE [Group] = @p0 AND [Number] = @p1
    UNION ALL
SELECT [Id], [ApiSource], [Group], [Name], [Number]
FROM pokemons
WHERE NOT EXISTS (
    SELECT 1
    FROM fnums
    WHERE fnums.Number = pokemons.Number
)";
            var pokemon = pokeflexContext.Pokemons.FromSqlRaw(sql, r.Next(0,Groups), r.Next(1, Numbers)).AsEnumerable();
            return pokemon.FirstOrDefault();
        }
        
        
        [Benchmark]
        [BenchmarkCategory("Raw")]
        public async Task<Pokemon> GetUnionWhereNotExistsRaw()
        {
            Random r = new Random();
            var pokeflexContext=DbContextFactory.DbContext.PokeflexContext;
            string sql = @"
SELECT [t].[Id], [t].[ApiSource], [t].[Group], [t].[Name], [t].[Number]
FROM (
    SELECT [p].[Id], [p].[ApiSource], [p].[Group], [p].[Name], [p].[Number]
    FROM [Pokemons] AS [p]
    WHERE ([p].[Group] = @p0) AND ([p].[Number] = @p1)
    UNION ALL
    SELECT [p0].[Id], [p0].[ApiSource], [p0].[Group], [p0].[Name], [p0].[Number]
    FROM [Pokemons] AS [p0]
    WHERE (([p0].[Group] = 0) AND ([p0].[Number] = @p1)) AND NOT (EXISTS (
        SELECT 1
        FROM [Pokemons] AS [p1]
        WHERE ([p1].[Group] = 1) AND ([p1].[Number] = [p0].[Number])))
) AS [t]";
            var pokemon = await pokeflexContext.Pokemons.FromSqlRaw(sql, r.Next(0,Groups), r.Next(1, Numbers)).FirstOrDefaultAsync();
            return pokemon;
        }

        [Benchmark]
        [BenchmarkCategory("Linq")]
        public async Task<Pokemon> GetCoalesceLinq()
        {
            Random r = new Random();
            var pokeflexContext = DbContextFactory.DbContext.PokeflexContext;
            var grp = r.Next(0, Groups);
            var number = r.Next(0, Numbers);
            var pokemon =
                from bse in pokeflexContext.Pokemons
                join flx in pokeflexContext.Pokemons on bse.Number equals flx.Number into flx
                from f in flx.DefaultIfEmpty()
                where bse.Group==grp && bse.Number==number
                select new Pokemon(){
                    Id= f != null? f.Id: bse.Id,
                    Number= f != null? f.Number: bse.Number,
                    Group= f != null? f.Group: bse.Group,
                    ApiSource= f != null? f.ApiSource: bse.ApiSource,
                    Name= f != null? f.Name: bse.Name,
                };
            return await pokemon.FirstOrDefaultAsync();
        }

        [Benchmark]
        [BenchmarkCategory("Raw")]
        public async Task<Pokemon> GetCoalesceRaw()
        {
            Random r = new Random();
            var pokeflexContext=DbContextFactory.DbContext.PokeflexContext;
            string sql = @"
SELECT 
    COALESCE( pk_flex.Id, pk_base.Id ) AS 'Id',
    COALESCE( pk_flex.Name, pk_base.Name ) AS 'Name',
    COALESCE( pk_flex.Number, pk_base.Number ) AS 'Number',
    COALESCE( pk_flex.[Group], pk_base.[Group] ) AS 'Group',
    COALESCE( pk_flex.ApiSource, pk_base.ApiSource ) AS 'ApiSource'
FROM pokemons AS pk_base
JOIN pokemons AS pk_flex ON pk_flex.Number=pk_base.Number
WHERE pk_base.Number=@p0 AND pk_flex.[Group]=@p1 AND pk_base.[Group]=0";
            var pokemon = await pokeflexContext.Pokemons.FromSqlRaw(sql, r.Next(0,Groups), r.Next(1, Numbers)).FirstOrDefaultAsync();
            return pokemon;
        }

        [Benchmark]
        [BenchmarkCategory("Linq")]
        public async Task<Pokemon> GetSequentialLinqWhereNotIn()
        {
            Random r = new Random();
            var pokeflexContext = DbContextFactory.DbContext.PokeflexContext;
            var grp = r.Next(0, Groups);
            var number = r.Next(0, Numbers);
            var pokemon = await (
                from pk in pokeflexContext.Pokemons
                where pk.Group == grp && pk.Number == number
                select pk)
                .FirstOrDefaultAsync();
            var pokemons = new [] {number};
            // comparing against -1 to force a mismatch
            pokemon = await (
                    from pk in pokeflexContext.Pokemons
                    where pk.Group == 0 && pk.Number == number && ! pokemons.Contains(number)
                    select pk)
                    .FirstOrDefaultAsync();
                
            return pokemon;
        }
        
        [Benchmark]
        [BenchmarkCategory("Linq")]
        public async Task<Pokemon> GetSequentialLinq()
        {
            Random r = new Random();
            var pokeflexContext = DbContextFactory.DbContext.PokeflexContext;
            var grp = r.Next(0, Groups);
            var number = r.Next(0, Numbers);
            var pokemon = await (
                from pk in pokeflexContext.Pokemons
                where pk.Group == grp && pk.Number == number
                select pk)
                .FirstOrDefaultAsync();
            // comparing against -1 to force a mismatch
            pokemon = await (
                    from pk in pokeflexContext.Pokemons
                    where pk.Group == 0 && pk.Number == number && pk.Number != -1
                    select pk)
                    .FirstOrDefaultAsync();
                
            return pokemon;
        }
        
//         [Benchmark]
//         [BenchmarkCategory("Raw")]
//         public async Task<Pokemon> GetSequentialRaw()
//         {
//             Random r = new Random();
//             var pokeflexContext=DbContextFactory.DbContext.PokeflexContext;
//             string sql = @"
// SELECT 
//     COALESCE( pk_flex.Id, pk_base.Id ) AS 'Id',
//     COALESCE( pk_flex.Name, pk_base.Name ) AS 'Name',
//     COALESCE( pk_flex.Number, pk_base.Number ) AS 'Number',
//     COALESCE( pk_flex.[Group], pk_base.[Group] ) AS 'Group',
//     COALESCE( pk_flex.ApiSource, pk_base.ApiSource ) AS 'ApiSource'
// FROM pokemons AS pk_base
// JOIN pokemons AS pk_flex ON pk_flex.Number=pk_base.Number
// WHERE pk_base.Number=@p0 AND pk_flex.[Group]=@p1 AND pk_base.[Group]=0";
//             var pokemon = await pokeflexContext.Pokemons.FromSqlRaw(sql, r.Next(0,Groups), r.Next(1, Numbers)).FirstOrDefaultAsync();
//             return pokemon;
//         }
    }
}
