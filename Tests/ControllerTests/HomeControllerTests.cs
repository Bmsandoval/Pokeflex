using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

namespace ControllerTests
{
    public class HomeControllerTests 
    {
        [Fact]
        public void TestingPOC()
        {
            Assert.True(true);
        }
        
        [Fact]
        public void HealthCheckWorks()
        {
            // Arrange
            var controller = new HomeController();
        
            // Act
            var result = controller.Health();
            var statusCode=Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCode.StatusCode);
        }
    }
}
