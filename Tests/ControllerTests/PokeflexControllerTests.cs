using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Controllers;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using App;
using App.Data;
using App.Models;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;

namespace Tests.ControllerTests
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
        public async Task Index_ReturnsListOfPokemons()
        {
            // Test Data
            var basemons = new List<Basemon>();
            basemons.Add(new Basemon()
            {
                ApiSource = "controller test",
                Number = 42,
                Name = "saltyboi",
            });
            
            // Arrange
            Mock<PokeflexService> mockPokeflexService = NewMockPokeflex();
            mockPokeflexService.Setup(repo => repo.GetByNumber(42)).Returns(default(Pokemon));
            mockPokeflexService.Setup(repo => repo.InsertPokemon((Pokemon)basemons[0])).Returns((Pokemon)basemons[0]);

            Mock<ExtPokeApiServiceFactoryProduct> mockExtPokeApisService = NewMockExtApis();
            mockExtPokeApisService.Setup(repo => repo.GetByNumber(42)).Returns(basemons[0]);
            
            var controller = new PokeflexController(mockExtPokeApisService.Object, mockPokeflexService.Object);
        
            // Act
            var result = controller.Index("42");
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((Pokemon)basemons[0], apiResult.Value);
        }
        
        // // GET: Pokemons
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _pokemonService.List());
        // }
        
        // [Fact]
        // public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        // {
        //     // Arrange
        //     var mockRepo = new Mock<PokemonService>();
        //     mockRepo.Setup(repo => repo.List()).ReturnsAsync(GetTestSessions());
        //     var controller = new PokemonsController(mockRepo.Object);
        //
        //     // Act
        //     var result = await controller.Index();
        //
        //     // Assert
        //     var viewResult = Assert.IsType<ViewResult>(result);
        //     var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
        //         viewResult.ViewData.Model);
        //     Assert.Equal(2, model.Count());
        // }
    }
}
