using System.Collections.Generic;
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
        public async Task<List<Student>> List()
        {
            return await _context.Students.ToListAsync();
        }
    }
}