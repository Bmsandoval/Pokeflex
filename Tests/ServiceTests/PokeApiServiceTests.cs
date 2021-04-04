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
            _responses.Enqueue( MockedWebResponseBuilder.MakeResponse( httpStatusCode, stream ) );
        }
    }
    
    
    public class PokeApiServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public void TestCanPullFromWeb()
        {
            // var pocomons = new List<Pocomon>()
            // {
            //     new Pocomon()
            //     {
            //         Number = 42,
            //         Name = "saltyboi",
            //     }
            // };
            //
            // var pcApiService = new MockPokeApiWebRequestSender();
            // var streamData = pocomons[0].ToJsonStream<Pocomon>();
            // pcApiService.AddResponse(HttpStatusCode.OK, streamData);
            // pcApiService.AddResponse(HttpStatusCode.OK, streamData);
            // var response = pcApiService.SendRequest(null);
            // Console.WriteLine("before");
            // Console.WriteLine(response);
            // Console.WriteLine(response.ContentLength);
            // Console.WriteLine("after");
            //
            // Basemon resultmon = pcApiService.GetByNumber(42);
            //
            // if (resultmon == default(Basemon))
            // {
            //     throw new Exception("unexpected default result returned");
            // }
            //
            // // List assert equal fails for some reason
            // Assert.Equal(pocomons[0], (Pocomon)resultmon);
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
