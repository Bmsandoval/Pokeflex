using App.Services.ExtPokeApis.PokeApiCo;
using Xunit;

namespace Tests.Units.ServiceTests.PokeApiServiceTests
{
    
    
    public class ServiceTests
    {
        [Theory]
        [MemberData(nameof(global::Tests.Units.ServiceTests.PokeApiServiceTests.ServiceDataGenerator.SingleInput), MemberType = typeof(global::Tests.Units.ServiceTests.PokeApiServiceTests.ServiceDataGenerator))]
        public void TestCanPullPocomonFromWeb(PokeapiCoService service, Pocomon pocomon)
        {
            Pocomon resultmon = (Pocomon)service.GetByNumber(42);
            
            Assert.Equal(pocomon.height, resultmon.height);
            Assert.Equal(pocomon.ApiSource, resultmon.ApiSource);
            Assert.Equal(pocomon, resultmon);
        }
    }
}
