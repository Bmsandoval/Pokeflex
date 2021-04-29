using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Tests.ServiceDataGenerator.Seeders;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class SelectTests
    {
        // SELECT NULL FROM EMPTY
        [Theory]
        [MemberData(nameof(PokemonSeeder.EmptyDatabase), MemberType = typeof(PokemonSeeder))]
        public async void TestCanSelectFromEmptyDb(PokeflexContext context)
        {
            var service = new PokeflexService(context);
            Pokemon pokemon = await service.Select(-1);
            Assert.Null(pokemon);
        }

        
        // SELECT BASE FROM MIXED
        [Theory]
        [MemberData(nameof(PokemonSeeder.MixedDatabase), MemberType = typeof(PokemonSeeder))]
        public async void TestCanSelectBaseFromMixedDb(PokeflexContext context, Pokemon basePk, Pokemon _)
        {
            var service = new PokeflexService(context);
            Pokemon resultmon = await service.Select(basePk.Number);
            Assert.NotNull(resultmon);
            Assert.Equal(basePk, resultmon);
        }
        
        
        // SELECT OVERRIDE FROM MIXED
        [Theory]
        [MemberData(nameof(PokemonSeeder.MixedDatabase), MemberType = typeof(PokemonSeeder))]
        public async void TestCanSelectOverrideFromMixedDb(PokeflexContext context, Pokemon _, Pokemon flexPk)
        {
            var service = new PokeflexService(context);
            Pokemon resultmon = await service.Select(flexPk.Number, flexPk.GroupId);
            Assert.True(context.Pokemons.Any());
            Assert.NotNull(resultmon);
            Assert.True(flexPk.Equals(resultmon));
        }
        
        
        // SELECT FROM SINGLE-TYPED
        [Theory]
        [MemberData(nameof(PokemonSeeder.BaseOnlyDatabase), MemberType = typeof(PokemonSeeder))]
        [MemberData(nameof(PokemonSeeder.FlexOnlyDatabase), MemberType = typeof(PokemonSeeder))]
        public async void TestCanSelectFromDbWithSingleTypes(PokeflexContext context, Pokemon exists)
        {
            var service = new PokeflexService(context);
            Pokemon resultmon = await service.Select(exists.Number, exists.GroupId);
            Assert.NotNull(resultmon);
            Assert.Equal(exists, resultmon);
        }
    }
}
