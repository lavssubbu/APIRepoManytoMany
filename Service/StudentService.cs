using APIRepoPattern.Interface;
using APIRepoPattern.Models;

namespace APIRepoPattern.Service
{
    public class StudentService
    {
        private readonly IStudent _studentRepository;

        public StudentService(IStudent repo)
        {
            _studentRepository = repo;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _studentRepository.GetByIdAsync(studentId);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentRepository.DeleteAsync(studentId);
        }
    }
}
