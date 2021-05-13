using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Models;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using NeinLinq;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Benchs.Proofs.Db
{
    public class EfSimpleQueriesBase : SingleQueriesBase
    {
        // [Params(5, 10, 15)] public int Groups;
        // [Params(10, 1_000, 10_000)] public int Numbers;
        [Params(15)] public override int Groups { get; set; }
        [Params(10, 100)] public override int Numbers { get; set; }
    }
    
    
    [BenchmarkCategory("Proofs", "Db", "EfSimpleQueries")]
    public class EfSimpleQueries : EfSimpleQueriesBase
    {
        // [BenchmarkCategory("NeinExpression")]
        // [Benchmark] public Pokemon NeinExpressions()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return  (
        //         from p in pokeflexContext.Pokemons.ToInjectable()
        //         where p.NeinInGroup(Group, Number)
        //             select p)
        //         .FirstOrDefault();
        // }
        //
        // [BenchmarkCategory("CachedNeinExpression")]
        // [Benchmark] public Pokemon CachedNeinExpressions()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return  (
        //         from p in pokeflexContext.Pokemons.ToInjectable()
        //         where p.CachedNeinInGroup(Group, Number)
        //             select p)
        //         .FirstOrDefault();
        // }
        
        [BenchmarkCategory("ManualPredicate")]
        [Benchmark] public Pokemon ManualPredicate()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemon = pokeflexContext.Pokemons.Where(SimpleExtensionLinqHelpers.PredicateExpression(Group, Number))
                .FirstOrDefault();
            Assert.NotNull(pokemon);
            return pokemon;
        }
        
//         [BenchmarkCategory("LambdaQuery")]
//         [Benchmark] public Pokemon LambdaQuery()
//         {
//             var pokeflexContext=DbContext.PokeflexContext;
//             return pokeflexContext.Pokemons.Where(ExtensionLinqHelpers.LambdaInGroup(Group, Number)).FirstOrDefault();
//         }
//         
//         [BenchmarkCategory("RawQuery")]
//         [Benchmark] public async Task<Pokemon> RawQuery()
//         {
//             var pokeflexContext=DbContext.PokeflexContext;
//             string sql = @"
// SELECT [p].[Id], [p].[ApiSource], [p].[GroupId], [p].[Name], [p].[Number]
// FROM [Pokemons] AS [p]
// WHERE ([p].[GroupId] = @p0) AND ([p].[Number] = @p1)";
//              return await pokeflexContext.Pokemons.FromSqlRaw(sql, Group, Number).FirstOrDefaultAsync();
//         }
        
        // [BenchmarkCategory("LinqQueryBaseline")]
        // [Benchmark] public Pokemon LinqQuerySyntax()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return  (
        //         from p in pokeflexContext.Pokemons
        //         where p.GroupId == Group
        //             select p)
        //         .FirstOrDefault();
        // }
        //
        // [BenchmarkCategory("LinqMethodBaseline")]
        // [Benchmark] public Pokemon LinqMethodSyntax()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return pokeflexContext.Pokemons.Where(p => p.GroupId == Group).FirstOrDefault();
        // }
        //
        // [BenchmarkCategory("BuiltPredicateExpression")]
        // [Benchmark] public async Task<Pokemon> BuiltPredicate()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     var pred = PredicateBuilder.True<Pokemon>().And(p=>p.GroupId==Group);
        //     return await pokeflexContext.Pokemons.Where(pred).FirstOrDefaultAsync();
        // }
        // [BenchmarkCategory("IQueryableExtension")]
        // [Benchmark] public Pokemon IQueryableExtension()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return pokeflexContext.Pokemons.IQueryableInGroup(Group).FirstOrDefault();
        // }

        // [BenchmarkCategory("LinqMethodExtendedExpression")]
        // [Benchmark] public Pokemon ExtendedLambdaMethodSyntax()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return pokeflexContext.Pokemons.Where(ExtensionLinqHelpers.LambdaInGroup(Group)).FirstOrDefault();
        // }
    }
    
    
    public static class SimpleExtensionLinqHelpers
    {
        public static Expression<Func<Pokemon, bool>> LambdaInGroup(int? group, int number) => p => p.GroupId == group && p.Number == number;
        
        
        public static Expression<Func<Pokemon, bool>> PredicateExpression(int? group, int number)=>
            PredicateBuilder.True<Pokemon>().And(p => p.GroupId == group && p.Number == number);
        
        
        [InjectLambda]
        public static bool NeinInGroup(this Pokemon pokemon, int? group, int number) => throw new NotImplementedException();
        public static Expression<Func<Pokemon, int?, int, bool>> NeinInGroup() => (p, g, number) => p.GroupId == g && p.Number == number;

        
        [InjectLambda]
        public static bool CachedNeinInGroup(this Pokemon pokemon, int? group, int number) => throw new NotImplementedException();
        public static CachedExpression<Func<Pokemon, int?, int, bool>> CachedNeinInGroup() =>
            CachedExpression.From<Func<Pokemon, int?, int, bool>>((p, g, number) => p.GroupId == g && p.Number == number);
        
        
        // private static IQueryable<Pokemon> InRange(this IQueryable<Pokemon> queryable, int skip, int take=default) => 
        //     queryable.Where(take == default
        //         ? p => p.Number == skip
        //         : p => p.Number > skip && p.Number <= skip + take);
        // public static IQueryable<Pokemon> Flexmons(this IQueryable<Pokemon> q, int? group, int skip, int take=default) => q
        //     .InGroup(group)
        //     .InRange(skip, take);
        // public static IOrderedQueryable<Pokemon> OrderByNum(this IQueryable<Pokemon> q, bool ascending=false) =>
        //     ascending ? q.OrderBy(pokemon => pokemon.Number) : q.OrderByDescending(pokemon => pokemon.Number);
        // public static IQueryable<Pokemon> ExceptExists(this IQueryable<Pokemon> q, IQueryable<Pokemon> pokemons, int? grp=null) =>
        //     from p in q
        //         where ! pokemons.Any(f => f.GroupId == grp && f.Number == p.Number)
        //         select p;
        // public static IQueryable<Pokemon> IncludeBasemons(this IQueryable<Pokemon> q, DbSet<Pokemon> db, int? flexGroup,
        //     int skip, int take = default) => q
        //     .Concat(db
        //     .Basemons(skip, take)
        //     .ExceptExists(db, flexGroup));
        // // private static IQueryable<T> Limit<T>(IQueryable<T> source, int limit) => source.TagWith("Limit").Take(limit);
        //
    }
}
