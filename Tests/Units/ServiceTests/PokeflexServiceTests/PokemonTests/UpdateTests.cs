using System;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class UpdateTests
    {
        // TEST A FEW OPERATIONS IN A ROW FROM SAME DB
        [Theory]
        [MemberData(nameof(Seeder.BasePokeOnlyDb), MemberType = typeof(Seeder))]
        public async void TestUpdate(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var poke = context.Pokemons.First();
            var service = new PokeflexService(context);
            poke.Name = Faker.Lorem.GetFirstWord();
            await service.Update(poke.GroupId, poke.Number, poke);
            Pokemon pk = await service.Select(poke.Number, poke.GroupId);
            Assert.NotNull(pk);
            Assert.Equal(poke, pk);
        }
    }
}
