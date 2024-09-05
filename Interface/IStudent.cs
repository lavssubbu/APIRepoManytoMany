using APIRepoPattern.Models;

namespace APIRepoPattern.Interface
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int studentId);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int studentId);
    }
}
