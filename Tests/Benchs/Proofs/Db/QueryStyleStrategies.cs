using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Shared;
using App.Models;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using NeinLinq;
using Xunit;

namespace Tests.Benchs.Proofs.Db
{
    public class QueryStyleStrategies : SingleQueriesBase
    {
        [Params(5, 10, 15)] public int Groups;
        [Params(10, 100, 1_000)] public int Numbers;
        // [Params(5)] public override int Groups { get; set; }
        // [Params(10)] public override int Numbers { get; set; }
    }
    
    
    [BenchmarkCategory("Proofs", "Db", "QueryStyleStrategies")]
    public class PreferQuerySyntaxAndNoCallToGetFancy : QueryStyleStrategies 
    {
        [Benchmark] public Pokemon NeinExpressions()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return  (
                from p in pokeflexContext.Pokemons.ToInjectable()
                where p.SimpleNeinInGroup(Group, Number)
                    select p)
                .FirstOrDefault();
        }
        
        [Benchmark] public Pokemon CachedNeinExpressions()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return  (
                from p in pokeflexContext.Pokemons.ToInjectable()
                where p.SimpleCachedNeinInGroup(Group, Number)
                    select p)
                .FirstOrDefault();
        }
        
        [Benchmark] public Pokemon ManualPredicate()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pokemon = pokeflexContext.Pokemons.Where(SimpleExtensionLinqHelpers.PredicateExpression(Group, Number))
                .FirstOrDefault();
            Assert.NotNull(pokemon);
            return pokemon;
        }
        
        [Benchmark] public Pokemon LinqQuerySyntax()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return  (
                from p in pokeflexContext.Pokemons
                where p.GroupId == Group && p.Number == Number
                    select p)
                .FirstOrDefault();
        }
        
        [Benchmark] public Pokemon LinqMethodSyntax()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return pokeflexContext.Pokemons.Where(p => p.GroupId == Group && p.Number == Number).FirstOrDefault();
        }
        
        [Benchmark] public Pokemon LinqMethodSyntaxShortcutFirst()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return pokeflexContext.Pokemons.FirstOrDefault(p => p.GroupId == Group && p.Number == Number);
        }
        
        [Benchmark] public async Task<Pokemon> BuiltPredicate()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            var pred = PredicateBuilder.And(PredicateBuilder.True<Pokemon>(), p=>p.GroupId==Group && p.Number == Number);
            return await pokeflexContext.Pokemons.Where(pred).FirstOrDefaultAsync();
        }
        [Benchmark] public Pokemon IQueryableExtension()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return pokeflexContext.Pokemons.GroupAndNum(Group, Number).FirstOrDefault();
        }

        [Benchmark] public Pokemon ExtendedLambdaMethodSyntax()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return pokeflexContext.Pokemons.Where(SimpleExtensionLinqHelpers.LambdaInGroup(Group, Number)).FirstOrDefault();
        }
    }
    
    
    public static class SimpleExtensionLinqHelpers
    {
        public static Expression<Func<Pokemon, bool>> LambdaInGroup(int? group, int number) => p => p.GroupId == group && p.Number == number;
        
        
        public static Expression<Func<Pokemon, bool>> PredicateExpression(int? group, int number)=>
            PredicateBuilder.And(PredicateBuilder.True<Pokemon>(), p => p.GroupId == group && p.Number == number);
        
        
        [InjectLambda]
        public static bool SimpleNeinInGroup(this Pokemon pokemon, int? group, int number) => throw new NotImplementedException();
        public static Expression<Func<Pokemon, int?, int, bool>> SimpleNeinInGroup() => (p, g, number) => p.GroupId == g && p.Number == number;

        
        [InjectLambda]
        public static bool SimpleCachedNeinInGroup(this Pokemon pokemon, int? group, int number) => throw new NotImplementedException();
        public static CachedExpression<Func<Pokemon, int?, int, bool>> SimpleCachedNeinInGroup() =>
            CachedExpression.From<Func<Pokemon, int?, int, bool>>((p, g, number) => p.GroupId == g && p.Number == number);
        
        
        public static IQueryable<Pokemon> GroupAndNum(this IQueryable<Pokemon> queryable, int? group, int number) => 
            queryable.Where(p => p.GroupId==group && p.Number==number);
    }
}
