using System.Collections.Generic;
using System.IO;
using System.Net;
using App.Data;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Units.ServiceTests.PokeApiServiceTests
{
    
    
    public class ServiceTests
    {
        [Theory]
        [MemberData(nameof(ServiceDataGenerator.SingleInput), MemberType = typeof(ServiceDataGenerator))]
        public void TestCanPullPocomonFromWeb(PokeapiCoService service, Pocomon pocomon)
        {
            Pocomon resultmon = (Pocomon)service.GetByNumber(42);
            
            Assert.Equal(pocomon.height, resultmon.height);
            Assert.Equal(pocomon.ApiSource, resultmon.ApiSource);
            Assert.Equal(pocomon, resultmon);
        }
    }
}
