using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using Xunit;

namespace Units.ServiceTests.PokeflexServiceTests
{
    public class SelectTests
    {
        // SELECT NULL FROM EMPTY
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.EmptyDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestCanSelectFromEmptyDb(PokeflexService service)
        {
            Pokemon pokemon = await service.Select(-1);
            Assert.Null(pokemon);
        }

        
        // SELECT BASE FROM MIXED
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.MixedDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestCanSelectBaseFromMixedDb(PokeflexService service, Pokemon basePk, Pokemon _)
        {
            Pokemon resultmon = await service.Select(basePk.Number);
            Assert.NotNull(resultmon);
            Assert.Equal(basePk, resultmon);
        }
        
        
        // SELECT OVERRIDE FROM MIXED
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.MixedDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestCanSelectOverrideFromMixedDb(PokeflexService service, Pokemon _, Pokemon flexPk)
        {
            Pokemon resultmon = await service.Select(flexPk.Number, flexPk.Group);
            Assert.NotNull(resultmon);
            Assert.Equal(flexPk, resultmon);
        }
        
        
        // SELECT FROM SINGLE-TYPED
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.BaseOnlyDatabase), MemberType = typeof(ServiceDataGenerator))]
        [MemberData(nameof(ServiceDataGenerator.FlexOnlyDatabase), MemberType = typeof(ServiceDataGenerator))]
        public async void TestCanSelectFromDbWithSingleTypes(PokeflexService service, Pokemon exists)
        {
            Pokemon resultmon = await service.Select(exists.Number, exists.Group);
            Assert.NotNull(resultmon);
            Assert.Equal(exists, resultmon);
        }
    }
}
