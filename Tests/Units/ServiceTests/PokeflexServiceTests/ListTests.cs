using System.Collections.Generic;
using App.Models;
using App.Services.Pokeflex;
using Xunit;

namespace Units.ServiceTests.PokeflexServiceTests
{
    public class ListTests
    {
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.EmptyDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestCanListEmptyDb(PokeflexService service)
        {
            List<Pokemon> resultmons = await service.GetRange(0, 1, 1);
            Assert.Empty(resultmons);
        }
    }
}
