using System;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Units.ServiceTests
{
    public class PokeflexServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public async void TestCanSelectOverrideFromMixedDb()
        {
            var pokemons = new List<Pokemon>
            {
                new () {
                    Group = 0,
                    ApiSource = "pokeapi.co",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 42
                },
                new () {
                    Group = 1,
                    ApiSource = "pokeapi.co",
                    Id = 1,
                    Name = "saltyboi",
                    Number = 42
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            
            Pokemon resultmon = await pokeflexService.Select(42, 1);

            if (resultmon.Equals(default(Pokemon)))
            {
                throw new Exception("unexpected default result returned");
            }

            // List assert equal fails for some reason
            Assert.Equal(pokemons[1], resultmon);
        }
        
        
        [Fact]
        public async void TestCanSelectBasemonFromMixedDb()
        {
            var pokemons = new List<Pokemon>()
            {
                new () {
                    Group = 0,
                    ApiSource = "pokeapi.co",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 42
                },
                new () {
                    Group = 1,
                    ApiSource = "pokeapi.co",
                    Id = 1,
                    Name = "saltyboi",
                    Number = 42
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            
            Pokemon resultmon = await pokeflexService.Select(42);

            Assert.NotNull(resultmon);
            Assert.Equal(pokemons[0], resultmon);
        }
        
        
        [Fact]
        public async void TestCanSelectBasemonFromBaseOnlyDb()
        {
            var pokemons = new List<Pokemon>
            {
                new () {
                    Group = 0,
                    ApiSource = "pokeapi.co",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 42
                },
                new () {
                    Group = 0,
                    ApiSource = "pokeapi.co",
                    Id = 1,
                    Name = "saltyboi",
                    Number = 43
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            
            Pokemon resultmon = await pokeflexService.Select(42, 1);

            if (resultmon.Equals(default(Pokemon)))
            {
                throw new Exception("unexpected default result returned");
            }

            Assert.NotNull(resultmon);
            Assert.Equal(pokemons[0], resultmon);
        }
        
        
        [Fact]
        public async void TestCanSelectBasemonFromFlexOnlyDb()
        {
            var pokemons = new List<Pokemon>()
            {
                new () {
                    Group = 1,
                    ApiSource = "pokeapi.co",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 42
                },
                new () {
                    Group = 1,
                    ApiSource = "pokeapi.co",
                    Id = 1,
                    Name = "saltyboi",
                    Number = 43
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            
            Pokemon resultmon = await pokeflexService.Select(42, 1);

            Assert.NotNull(resultmon);
            Assert.Equal(pokemons[0], resultmon);
        }
        
        
        [Fact]
        public async void TestCanGetListFromDb()
        {
            var pokemons = new List<Pokemon>()
            {
                new () {
                    Group = 0,
                    ApiSource = "pokeapi.co",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 1
                },
                new () {
                    Group = 1,
                    ApiSource = "pokeapi.co",
                    Id = 1,
                    Name = "saltyboi",
                    Number = 1
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            List<Pokemon> resultmons = await pokeflexService.GetRange(0, 1, 1);
            Assert.NotEmpty(resultmons);
        }
        
        [Fact]
        public async void TestIntegrations()
        {
            // SETUP DB
            var pokemons = new List<Pokemon>();
            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);

            // TEST EMPTY SELECT
            Pokemon testmon = await pokeflexService.Select(42, 1);
            Assert.Null(testmon);
            
            // INSERT NEW
            var inserted = new Pokemon { Group = 1, Number = 1, Name = Faker.Lorem.GetFirstWord() };
            await pokeflexService.Insert(inserted);

            // VALIDATE I CAN FIND IT
            testmon = await pokeflexService.Select(inserted.Number, inserted.Group);
            Assert.NotNull(testmon);
            Assert.Equal(inserted, testmon);

            // TRY TO UPDATE IT
            var updated = new Pokemon { Group = 2, Number = 2, Name = Faker.Lorem.GetFirstWord() };
            await pokeflexService.Update(inserted.Group, inserted.Number, updated);
            
            // GET IT BACK
            testmon = await pokeflexService.Select(2, 2);
            Assert.NotNull(testmon);
            Assert.Equal(updated, testmon);
            
            // VALIDATE THE OLD ONE IS GONE
            testmon = await pokeflexService.Select(1, 1);
            Assert.Null(testmon);
        }
    }
}
