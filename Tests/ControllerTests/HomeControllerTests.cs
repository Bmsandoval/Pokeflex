using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Controllers;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

namespace Tests.ControllerTests
{
    public class HomeControllerTests 
    {
        [Fact]
        public void HealthCheckWorks()
        {
            // Arrange
            // Act
            Assert.IsType<OkResult>((new HomeController()).Index());
        }
    }
}
