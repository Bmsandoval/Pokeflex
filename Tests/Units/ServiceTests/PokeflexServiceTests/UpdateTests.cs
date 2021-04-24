using App.Models;
using App.Services.Pokeflex;
using Xunit;

namespace Units.ServiceTests.PokeflexServiceTests
{
    public class UpdateTests
    {
        // TEST A FEW OPERATIONS IN A ROW FROM SAME DB
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.BaseOnlyDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestUpdate(PokeflexService service, Pokemon poke)
        {
            poke.Name = Faker.Lorem.GetFirstWord();
            await service.Update(poke.Group, poke.Number, poke);
            // int result = await service.Update(poke.Group, poke.Number, poke);
            // Assert.Equal(1,result);
            Pokemon pk = await service.Select(poke.Number, poke.Group);
            Assert.NotNull(pk);
            Assert.Equal(poke, pk);
        }
    }
}
