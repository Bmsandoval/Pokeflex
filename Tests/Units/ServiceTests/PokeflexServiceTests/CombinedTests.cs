using App.Models;
using App.Services.Pokeflex;
using Xunit;

namespace Units.ServiceTests.PokeflexServiceTests
{
    public class CombinedTests
    {
        // TEST A FEW OPERATIONS IN A ROW FROM SAME DB
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.EmptyDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestAllServiceFunctions(PokeflexService service)
        {
            // TEST EMPTY SELECT
            Pokemon testmon = await service.Select(42, 1);
            Assert.Null(testmon);
            
            // INSERT NEW
            var inserted = new Pokemon { Group = 1, Number = 1, Name = Faker.Lorem.GetFirstWord() };
            await service.Insert(inserted);
        
            // VALIDATE I CAN FIND IT
            testmon = await service.Select(inserted.Number, inserted.Group);
            Assert.NotNull(testmon);
            Assert.Equal(inserted, testmon);
        
            // TRY TO UPDATE IT
            var updated = new Pokemon { Group = 2, Number = 2, Name = Faker.Lorem.GetFirstWord() };
            await service.Update(inserted.Group, inserted.Number, updated);
            
            // SELECT IT BACK
            testmon = await service.Select(2, 2);
            Assert.NotNull(testmon);
            
            // LIST IT BACK
            var testmons = await service.GetRange(1, 1, 2);
            Assert.Single(testmons);
            
            // VALIDATE THE OLD ONE IS GONE
            testmon = await service.Select(1, 1);
            Assert.Null(testmon);
        }
    }
}