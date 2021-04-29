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

namespace Tests.Units.ControllerTests
{
    public class PokemonControllerTests
    {
        public static DbContextOptions<PokeflexContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<PokeflexContext>().Options;
        public Func<Mock<PokeflexService>> NewMockPokeflex { get; }= () =>
            new Mock<PokeflexService>(new PokeflexContext(DummyOptions));
        public Func<Mock<GroupService>> NewMockGroup { get; }= () =>
            new Mock<GroupService>(new PokeflexContext(DummyOptions));
        public Func<Mock<ExtPokeApiServiceFactoryProduct>> NewMockExtApis { get; }= () =>
            new Mock<ExtPokeApiServiceFactoryProduct>();
            
        
        [Fact]
        public async void Select_ReturnsPokemonByNumber()
        {
            // Test Data
            var pokemon = Pokemon.NewMock(null, 42);
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            Mock<GroupService> mockGroupService = NewMockGroup();
            
            mockPokeflexService.Setup(repo => repo.Select(42, null)).ReturnsAsync(default(Pokemon));
            mockExtPokeApisService.Setup(repo => repo.GetByNumber(42)).Returns(pokemon);
            mockPokeflexService.Setup(repo => repo.Insert(pokemon)).ReturnsAsync(1);
            
            var controller = new PokemonsController(mockExtPokeApisService.Object, mockPokeflexService.Object, mockGroupService.Object);
        
            // Act
            var result = await controller.Select(42);
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pokemon, apiResult.Value);
        }
        
        
        [Fact]
        public async void List_ReturnsListOfLocalPokemons()
        {
            // Test Data
            List<Pokemon> pokemons = new() { Pokemon.NewMock(null, 1) };
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            Mock<GroupService> mockGroupService = NewMockGroup();
            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            
            mockPokeflexService.Setup(repo => repo.GetRange(0, 1, 0)).ReturnsAsync(pokemons.ToList());
            
            var controller = new PokemonsController(mockExtPokeApisService.Object, mockPokeflexService.Object, mockGroupService.Object);
        
            // Act
            var result = await controller.List(0, 1, null);
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pokemons, apiResult.Value);
        }
    }
}
