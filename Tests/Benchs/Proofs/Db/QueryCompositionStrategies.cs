using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using NeinLinq;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Benchs.Proofs.Db
{
    public class QueryCompositionStrategies : SingleQueriesBase
    {
        // [Params(5, 10, 15)] public int Groups;
        // [Params(10, 100, 1_000)] public int Numbers;
        [Params(5)] public override int Groups { get; set; }
        [Params(10)] public override int Numbers { get; set; }
    }
    
    
    [BenchmarkCategory("Proofs", "Db", "QueryCompositionStrategies")]
    public class PreferQueryableWithExpressionsWhenFactoringOutQueryPieces : QueryCompositionStrategies 
    {
        [Benchmark] public async Task<List<Pokemon>> QueryableExtendedWithExpressions()
        {
            var pokeCtx=DbContext.PokeflexContext.Pokemons;
            return await pokeCtx
                .AFlexmons(Group,Number)
                .AIncludeBasemons(pokeCtx,Group,Number)
                .AOrderByNumber()
                .ToListAsync();
        }
        
        [Benchmark] public async Task<List<Pokemon>> QueryableExtendedWithLambdas()
        {
            var pokeCtx=DbContext.PokeflexContext.Pokemons;
            return await pokeCtx
                .BFlexmons(Group,Number)
                .BIncludeBasemons(pokeCtx,Group,Number)
                .BOrderByNumber()
                .ToListAsync();
        }
    }
    
    
    public static class CompositeExtensionLinqHelpers
    {
        /**
         * EXTENSIONS FOR PROOF
         */
        public static IQueryable<Pokemon> ABasemons(this IQueryable<Pokemon> q, int skip, int take=default) =>
            q.Flexmons(null, skip, take);
        public static IQueryable<Pokemon> AIncludeBasemons(this IQueryable<Pokemon> q, DbSet<Pokemon> db, int? flexGroup,
            int skip, int take = default) => q
            .Concat(db
            .ABasemons(skip, take)
            .AExceptExists(db, flexGroup));
        public static IOrderedQueryable<Pokemon> AOrderByNumber(this IQueryable<Pokemon> q, bool ascending=false) =>
            ascending ? q.OrderBy(pokemon => pokemon.Number) : q.OrderByDescending(pokemon => pokemon.Number);
        public static IQueryable<Pokemon> AExceptExists(this IQueryable<Pokemon> q, IQueryable<Pokemon> pokemons, int? grp=null) =>
            from p in q
                where ! pokemons.Any(f => f.GroupId == grp && f.Number == p.Number)
                select p;
        
        public static IQueryable<Pokemon> AFlexmons(this IQueryable<Pokemon> q, int? group, int skip, int take = default) => q
            .Where(PredicateBuilder.True<Pokemon>()
                .AInGroup(group)
                .AInRange(skip, take));
        
        private static Expression<Func<Pokemon, bool>> AInGroup(this Expression<Func<Pokemon,bool>> expr, int? group)=>
            PredicateTranslator.And(expr, p => p.GroupId == group);
        private static Expression<Func<Pokemon, bool>> AInRange(this Expression<Func<Pokemon, bool>> expr, int skip, int take = default) =>
            take == default
                ? PredicateTranslator.And(expr, p => p.Number == skip)
                : PredicateTranslator.And(expr, p => p.Number > skip && p.Number <= skip + take);
        
        
        /**
         * EXTENSIONS FROM PREVIOUS STATE
         */
        public static IQueryable<Pokemon> BBasemons(this IQueryable<Pokemon> q, int skip, int take=default) =>
            q.Flexmons(null, skip, take);
        private static IQueryable<Pokemon> BInGroup(this IQueryable<Pokemon> queryable, int? group) =>
            queryable.Where(p => p.GroupId == group);
        private static IQueryable<Pokemon> BInRange(this IQueryable<Pokemon> queryable, int skip, int take=default) => 
            queryable.Where(take == default
                ? p => p.Number == skip
                : p => p.Number > skip && p.Number <= skip + take);
        public static IQueryable<Pokemon> BFlexmons(this IQueryable<Pokemon> q, int? group, int skip, int take=default) => q
            .BInGroup(group)
            .BInRange(skip, take);
        public static IOrderedQueryable<Pokemon> BOrderByNumber(this IQueryable<Pokemon> q, bool ascending=false) =>
            ascending ? q.OrderBy(pokemon => pokemon.Number) : q.OrderByDescending(pokemon => pokemon.Number);
        public static IQueryable<Pokemon> BExceptExists(this IQueryable<Pokemon> q, IQueryable<Pokemon> pokemons, int? grp=null) =>
            from p in q
                where ! pokemons.Any(f => f.GroupId == grp && f.Number == p.Number)
                select p;
        public static IQueryable<Pokemon> BIncludeBasemons(this IQueryable<Pokemon> q, DbSet<Pokemon> db, int? flexGroup,
            int skip, int take = default) => q
            .Concat(db
            .BBasemons(skip, take)
            .BExceptExists(db, flexGroup));
    }
}
