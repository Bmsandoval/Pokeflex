using System;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;
using Tests.ServiceDataGenerator;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.GroupTests
{
    public class GroupInsertTests 
    {
        // TEST INSERT USER
        [Theory]
        [MemberData(nameof(Seeder.EmptyDb), MemberType = typeof(Seeder))]
        public async void TestInsertGroup(Mocker mocks)
        {
            var context = DbContextFactory.NewUniqueContext(GetType().Name, mocks).PokeflexContext;
            Assert.False(context.Groups.Any());
            var service = new GroupService(context);
            await service.Insert(Mocker.MockGroup());
            Assert.True(context.Groups.Any());
            Assert.Equal(1, (await context.Groups.SingleOrDefaultAsync()).Id);
            await service.Insert(Mocker.MockGroup());
            Assert.Equal(2, (await context.Groups.OrderByDescending(g=>g.Id).FirstOrDefaultAsync()).Id);
        }
    }
}