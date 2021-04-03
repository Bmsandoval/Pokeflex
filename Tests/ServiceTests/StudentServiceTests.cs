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
        public DbContextOptions<SchoolContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<SchoolContext>().Options;

        [Fact]
        public void TestBasicListFunctionality()
        {
            var students = new List<Student>()
            {
                new Student()
                {
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

            // List assert equal fails for some reason
            Assert.Equal(students, result);
        }

        // [Fact]
        // public void TestCanGetEFSql()
        // {
        //     var students = new List<Student>()
        //     {
        //         new Student()
        //         {
        //             EnrollmentDate = default,
        //             Enrollments = default,
        //             FirstMidName = default,
        //             Id = default,
        //             LastName = default
        //         },
        //     };
        //
        //     var dbContextMock = new DbContextMock<SchoolContext>(DummyOptions);
        //     var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Students, students.ToArray());
        //
        //     var studentService = new StudentService(dbContextMock.Object);
        //
        //     var result = studentService.Test();
        //
        //     // List assert equal fails for some reason
        //     Assert.Equal("select * from students where id = 1;", result);
        // }
        //
        // [Fact]
        // public void TestCanStillGetEFSql()
        // {
        //     var students = new List<Student>()
        //     {
        //         new Student()
        //         {
        //             EnrollmentDate = default,
        //             Enrollments = default,
        //             FirstMidName = default,
        //             Id = default,
        //             LastName = default
        //         },
        //     };
        //
        //     var dbContextMock = new DbContextMock<SchoolContext>(DummyOptions);
        //     var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Students, students.ToArray());
        //
        //     var studentService = new StudentService(dbContextMock.Object);
        //
        //     var result = studentService.Test();
        //
        //     // List assert equal fails for some reason
        //     Assert.Equal("select * from students where id = 1;", result);
        // }
    }
}
