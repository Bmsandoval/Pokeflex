using System;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Tests.ServiceDataGenerator.Seeders;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class UpdateTests
    {
        // TEST A FEW OPERATIONS IN A ROW FROM SAME DB
        [Theory]
        [MemberData(nameof(PokemonSeeder.BaseOnlyDatabase), MemberType = typeof(PokemonSeeder))]
        public async void TestUpdate(PokeflexContext context, Pokemon poke)
        {
            var service = new PokeflexService(context);
            poke.Name = Faker.Lorem.GetFirstWord();
            await service.Update(poke.GroupId, poke.Number, poke);
            Pokemon pk = await service.Select(poke.Number, poke.GroupId);
            Assert.NotNull(pk);
            Assert.Equal(poke, pk);
        }
    }
}
