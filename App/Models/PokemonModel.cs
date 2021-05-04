using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using App.Data;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NeinLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    [DataContract][Serializable][Index(nameof(GroupId), nameof(Number))]
    public class Pokemon : IPokemon
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public int? GroupId { get; set; }
        public Group Group { get; set; }
        [DataMember] public string ApiSource { get; set; }
        [DataMember] public int Number { get; set; }
        [DataMember] public string Name { get; set; }
        
        public Pokemon() { GroupId = 0; }

        public Pokemon(int? group=null) { GroupId = group; }
        
        public Pokemon(IPokemon pokemon, int? group=null)
        {
            GroupId = group;
            ApiSource = pokemon.ApiSource;
            Number = pokemon.Number;
            Name = pokemon.Name;
        }

        public override int GetHashCode()
        {
            return GroupId.GetHashCode() * 17 +
                   Number.GetHashCode() * 17 +
                   ApiSource.GetHashCode() * 17 +
                   Id.GetHashCode() * 17 +
                   Name.GetHashCode() * 17;
        }

        #nullable enable
        public override bool Equals(object? obj)
        {
            if (obj is not Pokemon testmon) { return false; }

            return Id == testmon.Id &&
                   GroupId == testmon.GroupId &&
                   Number == testmon.Number &&
                   ApiSource == testmon.ApiSource &&
                   Name == testmon.Name;
        }
        #nullable disable
    }

    public static class PokemonLinqHelpers
    {
        /*
         * Purpose: Find Pokemon(s) with @pkNumber in group 0
         */
        public static IQueryable<Pokemon> Basemons(this IQueryable<Pokemon> q, int skip, int take=default) =>
            q.Flexmons(null, skip, take);
        private static IQueryable<Pokemon> InGroup(this IQueryable<Pokemon> queryable, int? group) =>
            queryable.Where(p => p.GroupId == group);
        private static IQueryable<Pokemon> InRange(this IQueryable<Pokemon> queryable, int skip, int take=default) => 
            queryable.Where(take == default
                ? p => p.Number == skip
                : p => p.Number > skip && p.Number <= skip + take);
        public static IQueryable<Pokemon> Flexmons(this IQueryable<Pokemon> q, int? group, int skip, int take=default) => q
            .InGroup(group)
            .InRange(skip, take);
        public static IOrderedQueryable<Pokemon> OrderByNum(this IQueryable<Pokemon> q, bool ascending=false) =>
            ascending ? q.OrderBy(pokemon => pokemon.Number) : q.OrderByDescending(pokemon => pokemon.Number);
        public static IQueryable<Pokemon> ExceptExists(this IQueryable<Pokemon> q, IQueryable<Pokemon> pokemons, int? grp=null) =>
            from p in q
                where ! pokemons.Any(f => f.GroupId == grp && f.Number == p.Number)
                select p;
        public static IQueryable<Pokemon> IncludeBasemons(this IQueryable<Pokemon> q, DbSet<Pokemon> db, int? flexGroup,
            int skip, int take = default) => q
            .Concat(db
            .Basemons(skip, take)
            .ExceptExists(db, flexGroup));
        // private static IQueryable<T> Limit<T>(IQueryable<T> source, int limit) => source.TagWith("Limit").Take(limit);

    }
}
