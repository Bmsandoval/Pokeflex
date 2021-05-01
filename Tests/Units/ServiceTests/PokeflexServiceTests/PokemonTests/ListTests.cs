using System.Collections.Generic;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using TestSupport.EfHelpers;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class ListTests
    {
        [Theory]
        [MemberData(nameof(Seeder.EmptyDb), MemberType = typeof(Seeder))]
        public async void TestCanListEmptyDb(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var service = new PokeflexService(context);
            List<Pokemon> resultmons = await service.GetRange(0, 1, 1);
            Assert.Empty(resultmons);
        }
    }
}
