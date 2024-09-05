using APIRepoPattern.Interface;
using APIRepoPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRepoPattern.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly StudCourseDbContext _context;

        public CourseRepository(StudCourseDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
           return await _context.Courses.Include(s=>s.StudentCourses!).ThenInclude(s=>s.Student).ToListAsync() ?? throw new NullReferenceException();
        }

        public async Task<Course> GetByIdAsync(int courseId)
        {
            return await _context.Courses
           .Include(c => c.StudentCourses!)
           .ThenInclude(sc => sc.Student)
           .FirstOrDefaultAsync(c => c.CourseId == courseId) ?? throw new NullReferenceException();
        }

        public async Task UpdateAsync(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.CourseId);
            if (existingCourse == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }

            _context.Entry(existingCourse).CurrentValues.SetValues(course);
            await _context.SaveChangesAsync();
        }
    }
}
