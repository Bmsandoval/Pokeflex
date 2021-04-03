using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Controllers;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using App;
using App.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.ControllerTests
{
    public class PokemonControllerTests 
    {
        [Fact]
        public async Task Index_ReturnsListOfPokemons()
        {
            // Test Data
            var sessions = new List<Pokemon>();
            sessions.Add(new Pokemon()
            {
                EnrollmentDate = default,
                FirstMidName = default,
                Id = default,
                LastName = default
            });
            
            // Arrange
            var mockRepo = new Mock<PokemonService>(new PokeflexContext(new DbContextOptionsBuilder<PokeflexContext>().Options));
            mockRepo.Setup(repo => repo.List()).ReturnsAsync(sessions);
            var controller = new PokemonsController(mockRepo.Object);
        
            // Act
            var result = await controller.Index();
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sessions, apiResult.Value);
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
