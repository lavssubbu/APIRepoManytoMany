using APIRepoPattern.Models;

namespace APIRepoPattern.Interface
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int courseId);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int courseId);
    }
}
