using System;
using System.Collections.Generic;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;

namespace Units.ServiceTests.PokeflexServiceTests
{
    public class ServiceDataGenerator
    {
        private static PokeflexService GetTestService(Pokemon[] pokemons)
        {
            var dummyOptions = new DbContextOptionsBuilder<PokeflexContext>().Options;
            var dbContextMock = new DbContextMock<PokeflexContext>(dummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons);
            return new PokeflexService(dbContextMock.Object);
        }

        // EMPTY
        public static IEnumerable<object[]> EmptyDatabase()
        {
            yield return new object[] {GetTestService(Array.Empty<Pokemon>())};
        }

        // MIXED
        public static IEnumerable<object[]> MixedDatabase()
        {
            var basePk = new Pokemon
            {
                Group = 0,
                ApiSource = "pokeapi.co",
                Id = 0,
                Name = "saltyboi",
                Number = 42
            };
            var flexPk = new Pokemon
            {
                Group = 1,
                ApiSource = "pokeapi.co",
                Id = 1,
                Name = "saltyboi",
                Number = 42
            };
            yield return new object[] {GetTestService(new[] {basePk, flexPk}), basePk, flexPk};
        }

        // BASE ONLY
        public static IEnumerable<object[]> BaseOnlyDatabase()
        {
            var existsPk = new Pokemon
            {
                Group = 0,
                ApiSource = "Homemade",
                Id = 0,
                Name = Faker.Lorem.GetFirstWord(),
                Number = 42
            };
            yield return new object[]
            {
                GetTestService(new[]
                {
                    existsPk,
                    new()
                    {
                        Group = 0,
                        ApiSource = "Homemade",
                        Id = 1,
                        Name = Faker.Lorem.GetFirstWord(),
                        Number = 43
                    }
                }),
                existsPk
            };
        }

        // FLEX ONLY
        public static IEnumerable<object[]> FlexOnlyDatabase()
        {
            var existsPk = new Pokemon
            {
                Group = 1,
                ApiSource = "Homemade",
                Id = 0,
                Name = Faker.Lorem.GetFirstWord(),
                Number = 42
            };
            yield return new object[]
            {
                GetTestService(new[]
                {
                    existsPk,
                    new()
                    {
                        Group = 1,
                        ApiSource = "Homemade",
                        Id = 1,
                        Name = Faker.Lorem.GetFirstWord(),
                        Number = 43
                    }
                }),
                existsPk
            };
        }
    }
}