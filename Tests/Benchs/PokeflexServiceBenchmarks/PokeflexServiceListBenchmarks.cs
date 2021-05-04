using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using ListRandomizer;
using Microsoft.EntityFrameworkCore;
using Tests.ServiceDataGenerator;

namespace Tests.Benchs.PokeflexServiceBenchmarks
{
    public class PokeflexServiceListBase
    {
        // [Params( 5, 15)] public int Groups;
        // [Params(100, 1_000, 5_000)] public int Numbers;
        // [Params(0.10, 0.30)] public double LimitAsPctNumbers;
        [Params( 15)] public int Groups;
        [Params(5_000)] public int Numbers;
        [Params(0.30)] public double LimitAsPctNumbers;
        protected int Group;
        protected int Limit;
        protected int Offset;
        protected IDbContext DbContext;
        private readonly Random _rand = new ();
        [GlobalSetup] public void Setup() => DbContext = DbContextFactory
            .NewUniqueContext( GetType().Name, Mocker
                .HasGroups(Groups)
                .WithPokemons(Numbers));

        [IterationSetup] public void IterationSetup()
        {
            Limit = (int)(Numbers * LimitAsPctNumbers);
            Offset = _rand.Next(0, Numbers - Limit);
            Group = _rand.Next(1, Groups);
        }
    }
    
    
    [BenchmarkCategory("All", "Service", "Pokeflex", "Linq", "List")]
    [CategoriesColumn] public class PokeflexServiceLinqListBenchmarks : PokeflexServiceListBase
    {
        [Benchmark(Baseline = true)] public async Task<List<Pokemon>> Baseline()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await 
                (from pk in DbContext.PokeflexContext.Pokemons where pk.GroupId==Group select pk)
                .Skip(Offset)
                .Take(Limit)
                .ToListAsync();
        }
        
        
        [BenchmarkCategory("ActiveState")]
        [Benchmark] public async Task<List<Pokemon>> UnionWhereNotExists()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await service.GetRange(Offset, Limit, Group);
        }

        [Benchmark] public async Task<List<Pokemon>> Coalesce()
        {
            var pokeflexContext = DbContext.PokeflexContext;
            var pokemon =
                from bse in pokeflexContext.Pokemons
                join flx in pokeflexContext.Pokemons on bse.Number equals flx.Number into flx
                from f in flx.DefaultIfEmpty()
                where bse.GroupId==Group && bse.Number>Offset && bse.Number <= Offset+Limit
                select new Pokemon(){
                    Id= f != null? f.Id: bse.Id,
                    Number= f != null? f.Number: bse.Number,
                    Group= f != null? f.Group: bse.Group,
                    ApiSource= f != null? f.ApiSource: bse.ApiSource,
                    Name= f != null? f.Name: bse.Name,
                };
            return await pokemon.ToListAsync();
        }
    }
    
    
    [BenchmarkCategory("Service", "Pokeflex", "Raw", "List")][CategoriesColumn]
    public class PokeflexServiceRawListBenchmarks : PokeflexServiceListBase
    {
        [Benchmark(Baseline = true)] public async Task<List<Pokemon>> Baseline()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await 
                (from pk in DbContext.PokeflexContext.Pokemons where pk.GroupId==Group select pk)
                .Skip(Offset)
                .Take(Limit)
                .ToListAsync();
        }

        [Benchmark] public Pokemon UnionWhereNotExistsCte()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
WITH fnums ([Number]) AS ( 
    SELECT [Number]
    FROM pokemons
    WHERE [GroupId] = @p0 AND [Number] > @p1 AND [Number] <= @p2
)
SELECT [Id], [ApiSource], [GroupId], [Name], [Number]
FROM pokemons
WHERE [GroupId] = @p0 AND [Number] > @p1 AND [Number] <= @p2
    UNION ALL
SELECT [Id], [ApiSource], [GroupId], [Name], [Number]
FROM pokemons
WHERE NOT EXISTS (
    SELECT 1
    FROM fnums
    WHERE fnums.Number = pokemons.Number
)";
            var pokemon = pokeflexContext.Pokemons.FromSqlRaw(sql, Group, Offset, Offset+Limit).AsEnumerable();
            return pokemon.FirstOrDefault();
        }

        [Benchmark] public async Task<List<Pokemon>> Coalesce()
        {
            var service = new PokeflexService(DbContext.PokeflexContext);
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
SELECT 
    COALESCE( pk_flex.Id, pk_base.Id ) AS 'Id',
    COALESCE( pk_flex.Name, pk_base.Name ) AS 'Name',
    COALESCE( pk_flex.Number, pk_base.Number ) AS 'Number',
    COALESCE( pk_flex.[GroupId], pk_base.[GroupId] ) AS 'GroupId',
    COALESCE( pk_flex.ApiSource, pk_base.ApiSource ) AS 'ApiSource'
FROM pokemons AS pk_base
LEFT JOIN pokemons AS pk_flex ON pk_flex.Number=pk_base.Number AND pk_flex.[GroupId]=@p0
WHERE pk_base.[GroupId]=0 AND pk_base.[Number] > @p1 AND pk_base.[Number] <= @p2";
            return await pokeflexContext.Pokemons.FromSqlRaw(sql, Group, Limit, Limit+Offset).ToListAsync();
        }
    
    }
}
