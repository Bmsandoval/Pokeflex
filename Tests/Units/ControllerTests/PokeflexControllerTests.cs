using System;
using System.Collections.Generic;
using System.Linq;
using App.Controllers;
using App.Data;
using App.Engines;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Permissions;
using App.Services.Pokeflex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tests.ServiceDataGenerator;
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
        public Func<Mock<PokeflexEngine>> NewMockPokeflexEngine { get; }= () =>
            new Mock<PokeflexEngine>();
            
        
        [Fact]
        public async void Select_ReturnsPokemonByNumber()
        {
            // Test Data
            var pokemon = Mocker.MockPokemon(null, 42);
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            Mock<GroupService> mockGroupService = NewMockGroup();
            // Mock<PokeflexEngine> mockPokeflexEngine = NewMockPokeflexEngine();
            
            mockPokeflexService.Setup(repo => repo.Select(42, null)).ReturnsAsync(default(Pokemon));
            mockExtPokeApisService.Setup(repo => repo.GetByNumber(42)).Returns(pokemon);
            mockPokeflexService.Setup(repo => repo.Insert(pokemon)).ReturnsAsync(1);
            // mockPokeflexEngine.Setup(repo => repo.SelectPokemonInsertMissing(42, 0)).ReturnsAsync(true);
            PokeflexEngine pokeflexEngine = new PokeflexEngine(mockPokeflexService.Object, mockExtPokeApisService.Object);
            
            
            var controller = new PokemonsController(mockExtPokeApisService.Object, mockPokeflexService.Object, pokeflexEngine, mockGroupService.Object);
        
            // Act
            var result = controller.Select(42);
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pokemon, apiResult.Value);
        }
        
        
        [Fact]
        public async void List_ReturnsListOfLocalPokemons()
        {
            // Test Data
            List<Pokemon> pokemons = new() { Mocker.MockPokemon(null, 1) };
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            Mock<GroupService> mockGroupService = NewMockGroup();
            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            
            mockPokeflexService.Setup(repo => repo.GetRange(0, 1, null)).ReturnsAsync(pokemons.ToList());
            
            PokeflexEngine pokeflexEngine = new PokeflexEngine(mockPokeflexService.Object, mockExtPokeApisService.Object);
            
            
            var controller = new PokemonsController(mockExtPokeApisService.Object, mockPokeflexService.Object, pokeflexEngine, mockGroupService.Object);
        
            // Act
            var result = await controller.List(0, 1);
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pokemons, apiResult.Value);
        }
    }
}
