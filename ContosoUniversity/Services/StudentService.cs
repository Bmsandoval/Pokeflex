using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Services
{
    public class StudentService
    {
        private readonly SchoolContext _context;
        public StudentService(SchoolContext databaseContext) {
            this._context = databaseContext;
        }
        public virtual async Task<List<Student>> List()
        {
            return await _context.Students.ToListAsync();
        }

        public virtual string Test()
        {
            var query = from s in _context.Students
                where s.Id.Equals(1)
                select s;
            // var result = query.ToList();
            Console.WriteLine(query.ToQueryString());
            return query.ToString();
        }
    }
}