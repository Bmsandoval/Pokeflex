using System.Collections.Generic;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.PokemonTests
{
    public class CombinedTests
    {
        // TEST A FEW OPERATIONS IN A ROW FROM SAME DB
        [Theory]
        [MemberData(nameof(Seeder.EmptyDb), MemberType = typeof(Seeder))]
        public async void TestAllServiceFunctions(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            var pokeflexService = new PokeflexService(context);
            var groupsService = new GroupService(context);
            
            // TEST EMPTY SELECT
            Pokemon testmon = await pokeflexService.Select(42, 1);
            Assert.Null(testmon);
        
            var group = new Group() {Id = 1, Pokemons = new List<Pokemon>()};
            // INSERT NEW
            var inserted = new Pokemon { GroupId = 1, Number = 1, Name = Faker.Lorem.GetFirstWord(), Group = group};
            group.Pokemons.Add(inserted);
            await groupsService.Insert(group);
        
            // VALIDATE I CAN FIND IT
            testmon = await pokeflexService.Select(inserted.Number, inserted.GroupId);
            Assert.NotNull(testmon);
            Assert.Equal(inserted, testmon);
        
            // TRY TO UPDATE IT
            var updated = new Pokemon { GroupId = 1, Number = 2, Name = Faker.Lorem.GetFirstWord() };
            await pokeflexService.Update(inserted.GroupId, inserted.Number, updated);
            
            // SELECT IT BACK
            testmon = await pokeflexService.Select(2, 1);
            Assert.NotNull(testmon);
            
            // LIST IT BACK
            var testmons = await pokeflexService.GetRange(1, 1, 1);
            Assert.Single(testmons);
            
            // VALIDATE THE OLD ONE IS GONE
            testmon = await pokeflexService.Select(1, 1);
            Assert.Null(testmon);
        }
        //
        // // TEST MANY TO MANY
        // [Theory]
        // [MemberData(nameof(Seeder.EmptyDb), MemberType = typeof(Seeder))]
        // public async void TestManyToMany(Mocker mocks)
        // {
        //     var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
        //     var service = new PokeflexService(context);
        //     var result = await service.Testy();
        //     Assert.NotEmpty(result);
        // }
    }
}