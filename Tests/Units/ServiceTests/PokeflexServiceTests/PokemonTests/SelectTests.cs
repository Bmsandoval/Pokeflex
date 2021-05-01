using System;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class SelectTests
    {
        // SELECT NULL FROM EMPTY
        [Theory]
        [MemberData(nameof(Seeder.EmptyDb), MemberType = typeof(Seeder))]
        public async void TestCanSelectFromEmptyDb(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var service = new PokeflexService(context);
            Pokemon pokemon = await service.Select(-1);
            Assert.Null(pokemon);
        }
    
        
        // SELECT BASE FROM MIXED
        [Theory]
        [MemberData(nameof(Seeder.MixedPokeDb), MemberType = typeof(Seeder))]
        public async void TestCanSelectBaseFromMixedDb(Mocker mocks, int number)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var service = new PokeflexService(context);
            Pokemon resultmon = await service.Select(number);
            Assert.NotNull(resultmon);
            Assert.Null(resultmon.GroupId);
            Assert.Equal(number, resultmon.Number);
        }
        
        
        // SELECT OVERRIDE FROM MIXED
        [Theory]
        [MemberData(nameof(Seeder.MixedPokeDb), MemberType = typeof(Seeder))]
        public async void TestCanSelectOverrideFromMixedDb(Mocker mocks, int number)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var service = new PokeflexService(context);
            Pokemon resultmon = await service.Select(number, 1);
            Assert.NotNull(resultmon);
            Assert.NotNull(resultmon.GroupId);
            Assert.Equal(number, resultmon.Number);
        }
        
        
        // SELECT FROM SINGLE-TYPED
        [Theory]
        [MemberData(nameof(Seeder.BasePokeOnlyDb), MemberType = typeof(Seeder))]
        [MemberData(nameof(Seeder.FlexPokeOnlyDb), MemberType = typeof(Seeder))]
        public async void TestCanSelectFromDbWithSingleTypes(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var poke = context.Pokemons.First();
            var service = new PokeflexService(context);
            Pokemon resultmon = await service.Select(poke.Number, poke.GroupId);
            Assert.True(context.Pokemons.Any());
            Assert.NotNull(resultmon);
            Assert.Equal(poke, resultmon);
        }
    }
}
