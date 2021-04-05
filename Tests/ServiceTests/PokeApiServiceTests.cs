using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using App.Data;
using App.Models;
using App.Services;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Services.Pokeflex;
using App.Shared;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Newtonsoft.Json;

namespace Tests.ServiceTests
{
    public class MockPokeApiWebRequestSender: PokeapiCoService
    {
        private Queue<WebResponse> _responses = new ();
        public override WebResponse SendRequest(WebRequest _)
        {
            return _responses.Dequeue();
        }
        public void AddResponse(HttpStatusCode httpStatusCode, MemoryStream stream)
        {
            Mock<WebResponse> mockResponse = new();
            mockResponse.Setup(repo => repo.GetResponseStream()).Returns(stream);
            // var response = MockedWebResponseBuilder.MakeResponse(httpStatusCode, stream);
            _responses.Enqueue( mockResponse.Object );
        }
    }
    
    
    public class PokeApiServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public void TestCanPullFromWeb()
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
            pcApiService.AddResponse(HttpStatusCode.OK, streamData);
            Pokemon resultmon = pcApiService.GetByNumber(42);
            Assert.Equal(pocomon, resultmon);
        }

        // [Fact]
        // public void TestCanGetEFSql()
        // {
        //     var pokemons = new List<Pokemon>()
        //     {
        //         new Pokemon()
        //         {
        //             EnrollmentDate = default,
        //             FirstMidName = default,
        //             Id = default,
        //             LastName = default
        //         },
        //     };
        //
        //     var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
        //     var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
        //
        //     var pokemonService = new FlexmonService(dbContextMock.Object);
        //
        //     var result = pokemonService.Test();
        //
        //     // List assert equal fails for some reason
        //     Assert.Equal("select * from pokemons where id = 1;", result);
        // }
        //
        // [Fact]
        // public void TestCanStillGetEFSql()
        // {
        //     var pokemons = new List<Pokemon>()
        //     {
        //         new Pokemon()
        //         {
        //             EnrollmentDate = default,
        //             FirstMidName = default,
        //             Id = default,
        //             LastName = default
        //         },
        //     };
        //
        //     var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
        //     var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
        //
        //     var pokemonService = new FlexmonService(dbContextMock.Object);
        //
        //     var result = pokemonService.Test();
        //
        //     // List assert equal fails for some reason
        //     Assert.Equal("select * from pokemons where id = 1;", result);
        // }
    }
}
