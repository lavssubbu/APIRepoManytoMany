using APIRepoPattern.Models;

namespace APIRepoPattern.Interface
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int courseId);

        Task<Course> GetByNameAsync(string Name);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int courseId);
    }
}
