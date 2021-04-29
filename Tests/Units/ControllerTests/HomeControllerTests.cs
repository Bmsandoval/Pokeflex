using App.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Tests.Units.ControllerTests
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
