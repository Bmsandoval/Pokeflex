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
    public class StudentServiceTests
    {
        public DbContextOptions<SchoolContext> DummyOptions { get; } = new DbContextOptionsBuilder<SchoolContext>().Options;

        [Fact]
        public void TestBasicListFunctionality()
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

            for (var i = 0; i < students.Count; i++)
            {
                Assert.Equal(students[i].Id, result[i].Id);
                Assert.Equal(students[i].EnrollmentDate, result[i].EnrollmentDate);
                Assert.Equal(students[i].Enrollments, result[i].Enrollments);
                Assert.Equal(students[i].FirstMidName, result[i].FirstMidName);
                Assert.Equal(students[i].LastName, result[i].LastName);
            }
        }
    }
}
