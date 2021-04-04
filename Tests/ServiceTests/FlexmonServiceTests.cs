using System;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.Services;
using App.Services.ExtPokeApi.ApiFactoryBase;
using App.Services.Pokeflex;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace Tests.ServiceTests
{
    public class FlexmonServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public void TestCanPullFromDb()
        {
            var pokemons = new List<Pokemon>()
            {
                new Pokemon()
                {
                    ApiSource = "homegrown",
                    Id = 0,
                    Name = "saltyboi",
                    Number = 42,
                    Source = "PokemonTable"
                }
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());

            var flexmonService = new PokeflexService(dbContextMock.Object);
            Basemon resultmon = flexmonService.GetByNumber(42);

            if (resultmon == default(Basemon))
            {
                throw new Exception("unexpected default result returned");
            }

            Pokemon pokemon = new Pokemon(resultmon);

            // List assert equal fails for some reason
            Assert.Equal(pokemons[0], pokemon);
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
