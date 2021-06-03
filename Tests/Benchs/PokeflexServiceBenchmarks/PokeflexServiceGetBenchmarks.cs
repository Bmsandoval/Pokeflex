using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using NeinLinq;
using Tests.Benchs.Proofs.Db;
using Tests.ServiceDataGenerator;
using TestSupport.EfHelpers;
using Xunit;

namespace Tests.Benchs.PokeflexServiceBenchmarks
{
    public class PokeflexServiceGetBase
    {
        [Params(5, 10, 15)] public int Groups;
        [Params(10, 100, 1_000)] public int Numbers;
        // [Params(15)] public int Groups;
        // [Params(100)] public int Numbers;
        protected int Group;
        protected int Number;
        protected IDbContext DbContext;
        private Random _rand = new();

        [GlobalSetup]
        public void Setup()
        {
            DbContext = DbContextFactory
                .NewUniqueContext(
                    GetType().Name,
                    Mocker
                        .HasGroups(Groups)
                        .WithPokemons(Numbers));
        }

        [IterationSetup] public void IterationSetup()
        {
            Number = _rand.Next(1, Numbers);
            Group = _rand.Next(1, Groups);
        }
    }
    
    
    [BenchmarkCategory("All", "Service", "Pokeflex", "Linq", "Get")][CategoriesColumn]
    public class PokeflexServiceLinqGetBenchmarks : PokeflexServiceGetBase
    {
        [Benchmark] public async Task<Pokemon> BasicLinqQuery()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemon = await (
                    from p in pokeflexContext.Pokemons
                    where p.GroupId == Group && p.Number == Number select p)
                .FirstOrDefaultAsync();
            return pokemon;
        }
        
        [Benchmark] public async Task<Pokemon> BasicLinqMethod()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemons = await pokeflexContext
                .Pokemons
                .Where(p => p.GroupId == Group && p.Number == Number)
                .FirstOrDefaultAsync();
            return pokemons;
        }
        
        [Benchmark] public async Task<Pokemon> Proposal()
        {
            var pokeCtx=DbContext.PokeflexContext.Pokemons;
            var pokemon = await pokeCtx
                .AFlexmons(Group,Number)
                .AIncludeBasemons(pokeCtx,Group,Number)
                .FirstOrDefaultAsync();
            return pokemon;
        }

        [Benchmark] public async Task<Pokemon> UnionWhereNotExists()
        {
            var pokemon = await DbContext.PokeflexContext.Pokemons
                .Where(p => p.GroupId == Group && p.Number == Number)
                .Concat(
                    from p in DbContext.PokeflexContext.Pokemons
                    .Where(p => p.GroupId == null && p.Number == Number)
                    where ! DbContext.PokeflexContext.Pokemons
                        .Any(f => f.GroupId == Group && f.Number == p.Number)
                    select p).FirstOrDefaultAsync();
            return pokemon;
        }
        
        [Benchmark] public async Task<Pokemon> Coalesce()
        {
            var pokeflexContext = DbContext.PokeflexContext;
            var pokemon =
                from bse in pokeflexContext.Pokemons
                join flx in pokeflexContext.Pokemons on bse.Number equals flx.Number into flx
                from f in flx.DefaultIfEmpty()
                where bse.GroupId==Group && bse.Number==Number
                select new Pokemon {
                    Id= f != null? f.Id: bse.Id,
                    Number= f != null? f.Number: bse.Number,
                    Group= f != null? f.Group: bse.Group,
                    ApiSource= f != null? f.ApiSource: bse.ApiSource,
                    Name= f != null? f.Name: bse.Name,
                };
            return await pokemon.FirstOrDefaultAsync();
        }

        [Benchmark] public async Task<Pokemon> SequentialWhereNotIn()
        {
            var pokeflexContext = DbContext.PokeflexContext;
            var pokemon = await (
                from pk in pokeflexContext.Pokemons
                where pk.GroupId == Group && pk.Number == Number
                select pk)
                .FirstOrDefaultAsync();
            var pokemons = new [] {Number};
            // comparing against -1 to force a mismatch
            pokemon = await (
                    from pk in pokeflexContext.Pokemons
                    where pk.GroupId == null && pk.Number == Number && ! pokemons.Contains(Number)
                    select pk)
                    .FirstOrDefaultAsync();
                
            return pokemon;
        }
        
        [Benchmark] public async Task<Pokemon> SequentialWorstCase()
        {
            var pokeflexContext = DbContext.PokeflexContext;
            var pokemon = await (
                from pk in pokeflexContext.Pokemons
                where pk.GroupId == Group && pk.Number == Number
                select pk)
                .FirstOrDefaultAsync();
            // comparing against -1 to force a mismatch so we have to grab a second one
            pokemon = await (
                    from pk in pokeflexContext.Pokemons
                    where pk.GroupId == null && pk.Number == Number && pk.Number != -1
                    select pk)
                    .FirstOrDefaultAsync();
                
            return pokemon;
        }
        
        [Benchmark] public async Task<Pokemon> SqlServerStoredFunction()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemons = await pokeflexContext.SelectFlexmon(Group, Number).FirstOrDefaultAsync();
            return pokemons;
        }
        
    }
    
    
    [BenchmarkCategory("Service", "Pokeflex", "Raw", "Get")][CategoriesColumn]
    public class PokeflexServiceRawGetBenchmarks : PokeflexServiceGetBase
    {
        [Benchmark(Baseline = true)] public async Task<Pokemon> BaselineSimpleSelect()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
SELECT [p].[Id], [p].[ApiSource], [p].[GroupId], [p].[Name], [p].[Number]
FROM [Pokemons] AS [p]
WHERE ([p].[GroupId] = @p0) AND ([p].[Number] = @p1)";
            var pokemon = await pokeflexContext.Pokemons
                .FromSqlRaw(sql, Group, Number)
                .FirstOrDefaultAsync();
            return pokemon;
        }
        
        [Benchmark] public Pokemon CteUnionWhereNotExists()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
WITH fnums AS ( 
    SELECT [Number]
    FROM pokemons
    WHERE [GroupId] = @p0 AND [Number] = @p1
)
SELECT [Id], [ApiSource], [GroupId], [Name], [Number]
FROM pokemons
WHERE [GroupId] = @p0 AND EXISTS  (
    SELECT TOP 1 1
    FROM fnums
    WHERE fnums.Number = pokemons.Number
)
    UNION ALL
SELECT [Id], [ApiSource], [GroupId], [Name], [Number]
FROM pokemons
WHERE NOT EXISTS (
    SELECT TOP 1 1
    FROM fnums
    WHERE fnums.Number = pokemons.Number
)";
            var pokemon = pokeflexContext.Pokemons
                .FromSqlRaw(sql, Group, Number)
                .AsEnumerable().FirstOrDefault();
            return pokemon;
        }
        
        [Benchmark] public async Task<Pokemon> UnionWhereNotExists()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
SELECT [p].[Id], [p].[ApiSource], [p].[GroupId], [p].[Name], [p].[Number]
FROM [Pokemons] AS [p]
WHERE ([p].[GroupId] = @p0) AND ([p].[Number] = @p1)
UNION ALL
SELECT [p0].[Id], [p0].[ApiSource], [p0].[GroupId], [p0].[Name], [p0].[Number]
FROM [Pokemons] AS [p0]
WHERE (([p0].[GroupId] IS NULL) AND ([p0].[Number] = @p1)) AND NOT (EXISTS (
    SELECT TOP 1 1
    FROM [Pokemons] AS [p1]
    WHERE ([p1].[GroupId] = @p0) AND ([p1].[Number] = [p0].[Number])))
";
            var pokemon = await pokeflexContext.Pokemons
                .FromSqlRaw(sql, Group, Number)
                .FirstOrDefaultAsync();
            return pokemon;
        }
        
        [Benchmark] public async Task<Pokemon> Coalesce()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
SELECT 
    COALESCE( pk_flex.Id, pk_base.Id ) AS 'Id',
    COALESCE( pk_flex.Name, pk_base.Name ) AS 'Name',
    COALESCE( pk_flex.Number, pk_base.Number ) AS 'Number',
    COALESCE( pk_flex.[GroupId], pk_base.[GroupId] ) AS 'GroupId',
    COALESCE( pk_flex.ApiSource, pk_base.ApiSource ) AS 'ApiSource'
FROM pokemons AS pk_base
LEFT JOIN pokemons AS pk_flex ON pk_flex.Number=pk_base.Number
WHERE pk_base.Number=@p0 AND pk_flex.[GroupId]=@p1 AND pk_base.[GroupId]=0";
            var pokemon = await pokeflexContext.Pokemons
                .FromSqlRaw(sql, Group, Number)
                .FirstOrDefaultAsync();
            return pokemon;
        }
    }
}