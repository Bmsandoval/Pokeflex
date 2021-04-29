using System;
using System.Collections.Generic;
using App.Models;

namespace Tests.ServiceDataGenerator.Seeders
{
    public class PokemonSeeder
    {
        public static IEnumerable<object[]> EmptyDatabase()
        {
            yield return new object[] {DbContextFactory.NewDbContext(pokemons: Array.Empty<Pokemon>(), groups: Array.Empty<Group>()).PokeflexContext};
        }

        // MIXED
        public static IEnumerable<object[]> MixedDatabase()
        {
            var basePk = Pokemon.NewMock(null, 42);
            var flexPk = Pokemon.NewMock(1, 42);
            yield return new object[] {
                DbContextFactory.NewDbContext(
                    pokemons: new[] {basePk, flexPk}, 
                    groups: new[] {
                        new Group{Id=1}
                    }).PokeflexContext,
                basePk,
                flexPk
            };
        }

        // BASE ONLY
        public static IEnumerable<object[]> BaseOnlyDatabase()
        {
            var pokemons = new List<Pokemon> {
                Pokemon.NewMock(null, 42),
                Pokemon.NewMock(null, 43)
            };
            yield return new object[] {
                DbContextFactory.NewDbContext(
                    pokemons: pokemons.ToArray()).PokeflexContext,
                    pokemons[0]
            };
        }

        // FLEX ONLY
        public static IEnumerable<object[]> FlexOnlyDatabase()
        {
            var pokemons = new List<Pokemon> {
                Pokemon.NewMock(1, 42),
                Pokemon.NewMock(1, 43)
            };
            yield return new object[] {
                DbContextFactory.NewDbContext(
                    pokemons: pokemons.ToArray(),
                    groups: new [] {
                        new Group{Id=1},
                    }).PokeflexContext,
                pokemons[0]
            };
        }
    }
}
