using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Tests.ServiceDataGenerator;
using Tests.ServiceDataGenerator.Seeders;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.UserTests
{
    public class UserInsertTests 
    {
        // TEST INSERT USER
        [Theory]
        [MemberData(nameof(UserSeeder.EmptyDatabase), MemberType = typeof(UserSeeder))]
        public async void TestInsertUser(PokeflexContext context)
        {
            Assert.False(context.Users.Any());
            var service = new UserService(context);
            Assert.Equal(1, await service.Insert(User.NewMock()));
            Assert.True(context.Users.Any());
        }
    }
}