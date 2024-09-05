using APIRepoPattern.Interface;
using APIRepoPattern.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APIRepoPattern.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly StudCourseDbContext _context;

        public StudentRepository(StudCourseDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Student student)
        {
           await _context.Students.AddAsync(student);
           await _context.SaveChangesAsync();           
        }

        public async Task DeleteAsync(int studentId)
        {
            var stud = await _context.Students.FindAsync(studentId);
            if (stud != null)
            {
                _context.Students.Remove(stud);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Include(s => s.StudentCourses).ToListAsync() ?? throw new NotImplementedException();
        }

        public async Task<Student> GetByIdAsync(int studentId)
        {
            var student = await _context.Students
                                  .Include(s => s.StudentCourses!)
                                  .ThenInclude(c=>c.Course)
                                  .FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (student == null)
            {
                // Log or handle the case where the student is not found
                throw new KeyNotFoundException($"Student with ID {studentId} was not found.");
            }

            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
