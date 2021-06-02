using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using ListRandomizer;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Tests.Benchs.Proofs.Db;
using Tests.ServiceDataGenerator;
using Xunit;
using Xunit.Sdk;

namespace Tests.Benchs.PokeflexServiceBenchmarks
{
    public class PokeflexServiceListBase
    {
        [Params( 5, 15)] public int Groups;
        [Params(10, 100, 1_000)] public int Numbers;
        [Params(0.10, 0.30)] public double LimitAsPctNumbers;
        // [Params( 15)] public int Groups;
        // [Params(100)] public int Numbers;
        // [Params(0.30)] public double LimitAsPctNumbers;
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
            Offset = _rand.Next(1, Numbers - Limit);
            Group = _rand.Next(1, Groups);
        }
    }
    
    
    [BenchmarkCategory("All", "Service", "Pokeflex", "Linq", "List")]
    [CategoriesColumn] public class PokeflexServiceLinqListBenchmarks : PokeflexServiceListBase
    {
        [Benchmark(Baseline = true)] public async Task<List<Pokemon>> SelectListOfBases()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await (
                    from pk in DbContext.PokeflexContext.Pokemons 
                    where pk.GroupId==Group
                          && pk.Number>Offset 
                          && pk.Number <=Offset+Limit 
                    select pk)
                .OrderByNum()
                .Take(Limit)
                .ToListAsync();
        }
        
        [BenchmarkCategory("ActiveState")]
        [Benchmark] public async Task<List<Pokemon>> UnionWhereNotExists()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await (
                from flx in pokeflexContext.Pokemons
                    where flx.GroupId == Group && flx.Number > Offset && flx.Number <= Offset + Limit
                    select flx).
                Concat(
                from bse in pokeflexContext.Pokemons
                    where bse.GroupId == Group && bse.Number > Offset && bse.Number <= Offset + Limit
                          && !(from f in pokeflexContext.Pokemons
                                  where f.GroupId == Group select f.Number)
                              .Contains(bse.Number)
                select bse)
                .OrderByNum()
                .ToListAsync();
        }
        
        [Benchmark] public async Task<List<Pokemon>> LiveServiceCode()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await service.GetRange(Offset, Limit, Group);
        }

        [Benchmark] public List<Pokemon> SqlServerFuncInLinqQuery()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemons = (from ps in pokeflexContext.MakeRange(Offset+1, Offset+Limit)
                join fs in pokeflexContext.Pokemons.Where(pok=>pok.GroupId==Group) on ps.Number equals fs.Number
                join bs in pokeflexContext.Pokemons.Where(pok=>pok.GroupId==0) on new{name=fs.Name,num=ps.Number} equals new{name="",num=bs.Number}
                select fs ?? bs).ToList();
            return pokemons;
        }
        
        [Benchmark] public List<Pokemon> SqlServerFuncInLinqQueryCoalesced()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemons = (from ps in pokeflexContext.MakeRange(Offset+1, Offset+Limit)
                join fs in pokeflexContext.Pokemons.Where(pok=>pok.GroupId==Group) on ps.Number equals fs.Number
                join bs in pokeflexContext.Pokemons.Where(pok=>pok.GroupId==0) on new{name=fs.Name,num=ps.Number} equals new{name="",num=bs.Number}
                select new Pokemon(){
                    Id= fs != null? fs.Id: bs.Id,
                    Number= fs != null? fs.Number: bs.Number,
                    GroupId= fs != null? fs.GroupId: bs.GroupId,
                    ApiSource= fs != null? fs.ApiSource: bs.ApiSource,
                    Name= fs != null? fs.Name: bs.Name
                }).ToList();
            return pokemons;
        }
        
        
        [Benchmark] public async Task<List<Pokemon>> Coalesce()
        {
            var pokeflexContext = DbContext.PokeflexContext;
            var service = new PokeflexService(pokeflexContext);
            return await (from bse in (
                    from bs in pokeflexContext.Pokemons
                    where bs.GroupId == Group && bs.Number > Offset && bs.Number <= Offset + Limit select bs).OrderByNum()
                    join flx in 
                        from fl in pokeflexContext.Pokemons
                        where fl.GroupId == Group && fl.Number > Offset && fl.Number <= Offset + Limit
                        select fl
                        on bse.Number equals flx.Number into flx
                    from f in flx.DefaultIfEmpty()
                    select new Pokemon
                    {
                        Id = f != null ? f.Id : bse.Id,
                        Number = f != null ? f.Number : bse.Number,
                        Group = f != null ? f.Group : bse.Group,
                        ApiSource = f != null ? f.ApiSource : bse.ApiSource,
                        Name = f != null ? f.Name : bse.Name
                    })
                .Take(Limit)
                .OrderByNum()
                .ToListAsync();
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
        
        [Benchmark] public Pokemon UnionWhereNotExists()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            string sql = @"
SELECT [p].[Id], [p].[ApiSource], [p].[GroupId], [p].[Name], [p].[Number]
FROM pokemons AS [p]
WHERE [p].[GroupId] = @p0 AND [p].[Number] > @p1 AND [p].[Number] <= @p2
    UNION ALL
SELECT [p0].[Id], [p0].[ApiSource], [p0].[GroupId], [p0].[Name], [p0].[Number]
FROM pokemons AS [p0]
WHERE NOT EXISTS (
    SELECT TOP 1 1
    FROM pokemons 
    WHERE [GroupId]=@p0 AND [Number]=[p0].[NUMBER]
)";
            var pokemon = pokeflexContext.Pokemons.FromSqlRaw(sql, Group, Offset, Offset+Limit).AsEnumerable();
            return pokemon.FirstOrDefault();
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
