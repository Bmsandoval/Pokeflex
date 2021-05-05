using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using NeinLinq;
using Tests.Benchs.PokeflexServiceBenchmarks;
using Tests.ServiceDataGenerator;

namespace Tests.Benchs.Proofs
{
    public class NeinLinqVsCompiledPredicatesBase
    {
        // [Params(5, 10, 15)] public int Groups;
        // [Params(10, 1_000, 10_000)] public int Numbers;
        [Params(15)] public int Groups;
        [Params(10_000)] public int Numbers;
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
            Group = _rand.Next(0, Groups);
        }
    }
    
    
    [BenchmarkCategory("All", "Proofs", "LinqWhereLambda")][CategoriesColumn]
    public class PokeflexServiceExperimentalBenchmarks : PokeflexServiceGetBase
    {
        [BenchmarkCategory("NeinExpression")]
        [Benchmark] public Pokemon NeinExpressions()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return  (
                from p in pokeflexContext.Pokemons.ToInjectable()
                where p.NeinInGroup(Group)
                    select p)
                .FirstOrDefault();
        }
        //
        // [BenchmarkCategory("CompiledNeinExpression")]
        // [Benchmark] public Pokemon CompiledNeinExpressions()
        // {
        //     var pokeflexContext=DbContext.PokeflexContext;
        //     return  (
        //         from p in pokeflexContext.Pokemons.ToInjectable()
        //         where p.CompiledNeinInGroup(Group)
        //             select p)
        //         .FirstOrDefault();
        // }
        //
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

        [BenchmarkCategory("LinqMethodExtendedExpression")]
        [Benchmark] public Pokemon ExtendedLambdaMethodSyntax()
        {
            var pokeflexContext=DbContext.PokeflexContext;
            return pokeflexContext.Pokemons.Where(ExtensionLinqHelpers.LambdaInGroup(Group)).FirstOrDefault();
        }
    }
    
    
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T> ()  { return f => true;  }
        public static Expression<Func<T, bool>> False<T> () { return f => false; }
    
        public static Expression<Func<T, bool>> Or<T> (this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke (expr2, expr1.Parameters.Cast<Expression> ());
            return Expression.Lambda<Func<T, bool>>
                (Expression.OrElse (expr1.Body, invokedExpr), expr1.Parameters);
        }
    
        public static Expression<Func<T, bool>> And<T> (this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke (expr2, expr1.Parameters.Cast<Expression> ());
            return Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso (expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
    
    
    public static class ExtensionLinqHelpers
    {
        /*
         * Purpose: Find Pokemon(s) with @pkNumber in group 0
         */
        public static IQueryable<Pokemon> IQueryableInGroup(this IQueryable<Pokemon> q, int? group) =>
            q.Where(p=>p.GroupId == group);
        
        [InjectLambda]
        public static bool NeinInGroup(this Pokemon pokemon, int? group)
        {
            throw new NotImplementedException();
        }
        
        public static Expression<Func<Pokemon, int?, bool>> NeinInGroup()
        {
            return (p, g) => p.GroupId == g;
        }

        private static CachedExpression<Func<Pokemon, int?, bool>> CompiledNeinInGroupExpr { get; } =
            CachedExpression.From<Func<Pokemon, int?, bool>>((p, g) => p.GroupId == g);
            
        [InjectLambda]
        public static bool CompiledNeinInGroup(this Pokemon pokemon, int? group) =>
            CompiledNeinInGroupExpr.Compiled(pokemon, group);

        public static Expression<Func<Pokemon, int?, bool>> CompiledNeinInGroup()
        {
            return (p, g) => p.GroupId == g;
        }
        public static Expression<Func<Pokemon, bool>> LambdaInGroup(int? Group)
        {
            return p => p.GroupId == Group;
        }
        // public static Expression<Func<int?>> ExpressionInGroup(this IQueryable<Pokemon> q)
        // {
        //     return (q, g) => q.Where(p => p.GroupId == g);
        // }
        
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
