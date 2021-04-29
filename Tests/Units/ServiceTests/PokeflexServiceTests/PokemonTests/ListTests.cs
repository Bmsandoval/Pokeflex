using System.Collections.Generic;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Tests.ServiceDataGenerator.Seeders;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class ListTests
    {
        [Theory]
        [MemberData(nameof(PokemonSeeder.EmptyDatabase), MemberType = typeof(PokemonSeeder))]
        public async void TestCanListEmptyDb(PokeflexContext context)
        {
            var service = new PokeflexService(context);
            List<Pokemon> resultmons = await service.GetRange(0, 1, 1);
            Assert.Empty(resultmons);
        }
    }
}
