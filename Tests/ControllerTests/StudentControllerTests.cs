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
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.ControllerTests
{
    public class StudentControllerTests 
    {
        [Fact]
        public async Task Index_ReturnsListOfStudents()
        {
            // Test Data
            var sessions = new List<Student>();
            sessions.Add(new Student()
            {
                EnrollmentDate = default,
                Enrollments = default,
                FirstMidName = default,
                Id = default,
                LastName = default
            });
            
            // Arrange
            var mockRepo = new Mock<StudentService>(new SchoolContext(new DbContextOptionsBuilder<SchoolContext>().Options));
            mockRepo.Setup(repo => repo.List()).ReturnsAsync(sessions);
            var controller = new StudentsController(mockRepo.Object);
        
            // Act
            var result = await controller.Index();
        
            // Assert
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sessions, apiResult.Value);
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
    }
}
