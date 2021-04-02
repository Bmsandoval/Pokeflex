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
    public class StudentControllerTests 
    {
        [Fact]
        public void TestingPOC()
        {
            Assert.True(true);
        }
        
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
