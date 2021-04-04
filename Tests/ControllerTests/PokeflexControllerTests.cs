using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Controllers;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using App;
using App.Data;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;

namespace Tests.ControllerTests
{
    public class PokemonControllerTests 
    {
        [Fact]
        public async Task Index_ReturnsListOfPokemons()
        {
            // Test Data
            var pokemons = new List<Pokemon>();
            pokemons.Add(new Pokemon()
            {
                ApiSource = "homegrown",
                Id = 0,
                Name = "saltyboi",
                Number = 42,
                Source = "PokemonTable"
            });
            
            // Arrange
            var mockPokeflexService = new Mock<PokeflexService>(new PokeflexContext(new DbContextOptionsBuilder<PokeflexContext>().Options));
            mockPokeflexService.Setup(repo => repo.GetByNumber(200)).Returns(default(Pokemon));
            
            var mockExtPokeApiService = new Mock<PokeflexService>(new PokeflexContext(new DbContextOptionsBuilder<PokeflexContext>().Options));
            mockExtPokeApiService.Setup(repo => repo.GetByNumber(200)).Returns(pokemons[0]);
            var controller = new PokemonsController(mockPokeflexService.Object, mockExtPokeApiService.Object);
        
            // Act
            var result = controller.Index();
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pokemons[0], apiResult.Value);
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
