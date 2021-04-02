using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using ContosoUniversity;

namespace ControllerTests
{
    public class StudentControllerTests 
    {
        [Fact]
        public void TestingPOC()
        {
            Assert.True(true);
        }
        
            // // Arrange
            // var controller = new HomeController();
            //
            // // Act
            // var result = controller.Health();
            // var statusCode=Assert.IsType<StatusCodeResult>(result);
            // Assert.Equal(200, statusCode.StatusCode);
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<StudentService>();
            mockRepo.Setup(repo => repo.List()).ReturnsAsync(GetTestSessions());
            var controller = new StudentsController(mockRepo.Object);
        
            // Act
            var result = await controller.Index();
        
            // Assert
            var viewResult = Assert.IsType<List<Student>>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult);
            // var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //     viewResult.ViewData.Model);
            Assert.Equal(2, models.Count());
        }
        
        // // GET: Students
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _studentService.List());
        // }
        
        // [Fact]
        // public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        // {
        //     // Arrange
        //     var mockRepo = new Mock<StudentService>();
        //     mockRepo.Setup(repo => repo.List()).ReturnsAsync(GetTestSessions());
        //     var controller = new StudentsController(mockRepo.Object);
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
        
        #region snippet_GetTestSessions
        private List<Student> GetTestSessions()
        {
            var sessions = new List<Student>();
            sessions.Add(new Student()
            {
                EnrollmentDate = default,
                Enrollments = default,
                FirstMidName = default,
                ID = default,
                LastName = default
            });
            return sessions;
        }
        #endregion
    }
}
