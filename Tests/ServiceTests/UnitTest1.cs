using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace ServiceTests
{
    public class UnitTest1
    {
        public DbContextOptions<SchoolContext> DummyOptions { get; } = new DbContextOptionsBuilder<SchoolContext>().Options;

        
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestyTest()
        {
            var students = new List<Student>() {
                new Student(){
                    EnrollmentDate = default,
                    Enrollments = default,
                    FirstMidName = default,
                    Id = default,
                    LastName = default
                },
            };
        
            var dbContextMock = new DbContextMock<SchoolContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Students, students.ToArray());
            
            var studentService = new StudentService(dbContextMock.Object);

            var result = studentService.List().Result;
            
            Assert.Equal(students, result);
        }
    }
}
