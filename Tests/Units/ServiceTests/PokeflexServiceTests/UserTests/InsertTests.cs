using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.UserTests
{
    public class UserInsertTests 
    {
        // TEST INSERT USER
        [Theory]
        [MemberData(nameof(Seeder.EmptyDb), MemberType = typeof(Seeder))]
        public async void TestInsertUser(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            Assert.False(context.AppUsers.Any());
            var service = new UserService(context);
            Assert.Equal(1, await service.Insert(Mocker.MockUser()));
            Assert.True(context.AppUsers.Any());
        }
    }
}