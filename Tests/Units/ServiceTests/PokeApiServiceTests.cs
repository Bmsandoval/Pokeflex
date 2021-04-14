using System.Collections.Generic;
using System.IO;
using System.Net;
using App.Data;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Units.ServiceTests
{
    public class MockPokeApiWebRequestSender: PokeapiCoService
    {
        private Queue<WebResponse> _responses = new ();
        public override WebResponse SendRequest(WebRequest _)
        {
            return _responses.Dequeue();
        }
        public void AddResponse(MemoryStream stream)
        {
            Mock<WebResponse> mockResponse = new();
            mockResponse.Setup(repo => repo.GetResponseStream()).Returns(stream);
            _responses.Enqueue( mockResponse.Object );
        }
    }
    
    
    public class PokeApiServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public void TestCanPullPocomonFromWeb()
        {
            Pocomon pocomon = new()
            {
                height = 2,
                ApiSource = "pokeapi.co",
                Number = 42,
                Name = "saltyboi",
            };
            
            var pcApiService = new MockPokeApiWebRequestSender();
            var streamData = StreamHelpers.ToJsonStream(pocomon);
            pcApiService.AddResponse(streamData);
            Pocomon resultmon = (Pocomon)pcApiService.GetByNumber(42);
            
            Assert.Equal(pocomon.height, resultmon.height);
            Assert.Equal(pocomon.ApiSource, resultmon.ApiSource);
            Assert.Equal(pocomon, resultmon);
        }
    }
}
