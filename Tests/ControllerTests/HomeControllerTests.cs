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
        public void HealthCheckWorks()
        {
            // Arrange
            // Act
            Assert.IsType<OkResult>((new HomeController()).Health());
        }
    }
}
