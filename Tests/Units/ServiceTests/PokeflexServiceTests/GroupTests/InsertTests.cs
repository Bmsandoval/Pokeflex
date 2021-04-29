using System;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;
using Tests.ServiceDataGenerator;
using Tests.ServiceDataGenerator.Seeders;
using Xunit;

namespace Tests.Units.ServiceTests.PokeflexServiceTests.GroupTests
{
    public class GroupInsertTests 
    {
        // TEST INSERT USER
        [Theory]
        [MemberData(nameof(GroupSeeder.EmptyDatabase), MemberType = typeof(GroupSeeder))]
        public async void TestInsertGroup(PokeflexContext context)
        {
            Assert.False(context.Groups.Any());
            var service = new GroupService(context);
            await service.Insert(Group.NewMock());
            Assert.True(context.Groups.Any());
            Assert.Equal(1, (await context.Groups.SingleOrDefaultAsync()).Id);
            await service.Insert(Group.NewMock());
            Assert.Equal(2, (await context.Groups.OrderByDescending(g=>g.Id).FirstOrDefaultAsync()).Id);
        }
    }
}