using System;
using System.Collections.Generic;
using System.Linq;
using App.Controllers;
using App.Data;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Units.ControllerTests
{
    public class PokemonControllerTests
    {
        public static DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;
        public Func<Mock<PokeflexService>> NewMockPokeflex { get; }= () =>
            new Mock<PokeflexService>(new PokeflexContext(DummyOptions));
        public Func<Mock<ExtPokeApiServiceFactoryProduct>> NewMockExtApis { get; }= () =>
            new Mock<ExtPokeApiServiceFactoryProduct>();
            
        
        [Fact]
        public async void Select_ReturnsPokemonByNumber()
        {
            // Test Data
            Pokemon pokemon = new()
            {
                ApiSource = "controller test",
                Number = 42,
                Name = "saltyboi",
            };
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            
            mockPokeflexService.Setup(repo => repo.Select(42, 0)).ReturnsAsync(default(Pokemon));
            mockExtPokeApisService.Setup(repo => repo.GetByNumber(42)).Returns(pokemon);
            mockPokeflexService.Setup(repo => repo.Insert(pokemon)).ReturnsAsync(1);
            
            var controller = new PokemonsController(mockExtPokeApisService.Object, mockPokeflexService.Object);
        
            // Act
            var result = await controller.Select(42);
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            // Pokemon resultmon = StreamHelpers.FromJsonString<Pokemon>(apiResult.Value);
            Assert.Equal(pokemon, apiResult.Value);
        }
        
        
        [Fact]
        public async void List_ReturnsListOfLocalPokemons()
        {
            // Test Data
            List<Pokemon> pokemons = new()
            {
                new Pokemon() {
                    ApiSource = "controller test",
                    Number = 1,
                    Name = "saltyboi",
                    Group = 0
                }
            };
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            
            mockPokeflexService.Setup(repo => repo.GetRange(0, 1, 0)).ReturnsAsync(pokemons.ToList());
            
            var controller = new PokemonsController(mockExtPokeApisService.Object, mockPokeflexService.Object);
        
            // Act
            var result = await controller.List(0, 1, 0);
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pokemons, apiResult.Value);
        }
    }
}
