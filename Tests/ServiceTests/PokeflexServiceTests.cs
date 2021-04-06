using System;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.Models;
using App.Services;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace Tests.ServiceTests
{
    public class PokeflexServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public void TestCanPullFromDb()
        {
            var pokemons = new List<Pokemon>()
            {
                new ()
                {
                    ApiSource = "pokeapi.co",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 42
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());
            var pokeflexService = new PokeflexService(dbContextMock.Object);
            
            Pokemon resultmon = pokeflexService.GetByNumber(42);

            if (resultmon.Equals(default(Pokemon)))
            {
                throw new Exception("unexpected default result returned");
            }

            // List assert equal fails for some reason
            Assert.Equal(pokemons[0], resultmon);
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
