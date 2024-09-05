using APIRepoPattern.Interface;
using APIRepoPattern.Models;
using APIRepoPattern.Repository;

namespace APIRepoPattern.Service
{
    public class CourseService 
    {
        private readonly ICourse _courseRepository;

        public CourseService(ICourse repo)
        {
            _courseRepository = repo;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _courseRepository.GetByIdAsync(courseId);
        }

        public async Task AddCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            await _courseRepository.DeleteAsync(courseId);
        }
    }
}
