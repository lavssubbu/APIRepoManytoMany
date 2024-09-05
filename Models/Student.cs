namespace APIRepoPattern.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudName { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; }
    }
}
