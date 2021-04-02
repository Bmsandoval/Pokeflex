using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace ServiceTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        // [Fact]
        // public async void TestyTest()
        // {
        //     var options = new DbContextOptionsBuilder<SchoolContext>()
        //         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //         .Options;
        //     var databaseContext = new SchoolContext(options);
        //     databaseContext.Database.EnsureCreated();
        //     if (await databaseContext.Students.CountAsync() <= 0)
        //     {
        //         for (int i = 1; i <= 10; i++)
        //         {
        //             databaseContext.Students.Add(new Student(){
        //                 EnrollmentDate = default,
        //                 Enrollments = default,
        //                 FirstMidName = default,
        //                 ID = default,
        //                 LastName = default
        //                 
        //             });
        //             await databaseContext.SaveChangesAsync();
        //         }
        //     }
        //     
        //     var studentService = new StudentService(databaseContext);
        // }
    }
}
