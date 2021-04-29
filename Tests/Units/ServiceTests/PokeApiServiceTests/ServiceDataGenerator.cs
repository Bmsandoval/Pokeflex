using System.Collections.Generic;
using System.IO;
using System.Net;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Moq;

namespace Tests.Units.ServiceTests.PokeApiServiceTests
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
    
    
    public class ServiceDataGenerator
    {
        private static PokeapiCoService GetTestServiceWithSingle(Pocomon pocomon)
        {
            var pcApiService = new MockPokeApiWebRequestSender();
            var streamData = StreamHelpers.ToJsonStream(pocomon);
            pcApiService.AddResponse(streamData);
            return pcApiService;
        }

        
        // SINGLE
        public static IEnumerable<object[]> SingleInput()
        {
            var poco = new Pocomon
            {
                height = 2,
                ApiSource = "pokeapi.co",
                Number = 42,
                Name = "saltyboi"
            };
            yield return new object[]
            {
                GetTestServiceWithSingle(poco),
                poco
            };
        }
    }
}