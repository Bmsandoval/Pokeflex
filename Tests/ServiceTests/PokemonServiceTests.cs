using System;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.Models;
using App.Services;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace Tests.ServiceTests
{
    public class PokemonServiceTests
    {
        public DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;

        [Fact]
        public void TestBasicListFunctionality()
        {
            var pokemons = new List<Pokemon>()
            {
                new Pokemon()
                {
                    EnrollmentDate = default,
                    FirstMidName = default,
                    Id = default,
                    LastName = default
                },
            };

            var dbContextMock = new DbContextMock<PokeflexContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons.ToArray());

            var pokemonService = new PokemonService(dbContextMock.Object);

            var result = pokemonService.List().Result;

            // List assert equal fails for some reason
            Assert.Equal(pokemons, result);
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
        //     var pokemonService = new PokemonService(dbContextMock.Object);
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
        //     var pokemonService = new PokemonService(dbContextMock.Object);
        //
        //     var result = pokemonService.Test();
        //
        //     // List assert equal fails for some reason
        //     Assert.Equal("select * from pokemons where id = 1;", result);
        // }
    }
}
