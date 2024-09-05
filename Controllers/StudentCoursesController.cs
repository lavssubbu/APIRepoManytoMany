using APIRepoPattern.Models;
using APIRepoPattern.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIRepoPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public StudentCoursesController(StudentService studentService, CourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        // POST: api/StudentCourses
        [HttpPost]

        public async Task<IActionResult> EnrollStudentInCourse([FromBody] StudentCourse studentCourse)
        {
            if (studentCourse == null)
            {
                return BadRequest("StudentCourse cannot be null");
            }

            // Fetch the student and course to verify they exist
            var student = await _studentService.GetStudentByIdAsync(studentCourse.StudentId);
            var course = await _courseService.GetCourseByIdAsync(studentCourse.CourseId);

            if (student == null || course == null)
            {
                return NotFound("Student or Course not found");
            }

            // Check if the student is already enrolled in the course
            var existingEnrollment = student.StudentCourses?
                                            .FirstOrDefault(sc => sc.CourseId == studentCourse.CourseId);
            if (existingEnrollment != null)
            {
                return Conflict("Student is already enrolled in this course");
            }

            // Add the student-course relationship
            student.StudentCourses?.Add(studentCourse);
            course.StudentCourses?.Add(studentCourse);

            // Update both the student and course entities in the database
            await _studentService.UpdateStudentAsync(student);
            await _courseService.UpdateCourseAsync(course);

            return CreatedAtAction(nameof(GetStudentCoursesByStudentId), new { studentId = studentCourse.StudentId }, studentCourse);
        }

        // DELETE: api/StudentCourses
        [HttpDelete]
        public async Task<IActionResult> UnenrollStudentFromCourse([FromBody] StudentCourse studentCourse)
        {
            if (studentCourse == null) return BadRequest("StudentCourse cannot be null");

            var student = await _studentService.GetStudentByIdAsync(studentCourse.StudentId);
            var course = await _courseService.GetCourseByIdAsync(studentCourse.CourseId);

            if (student == null || course == null) return NotFound("Student or Course not found");

            // Remove the enrollment logic here
            student.StudentCourses!.Remove(studentCourse);
            course.StudentCourses!.Remove(studentCourse);

            await _studentService.UpdateStudentAsync(student); // Update the student entity
            await _courseService.UpdateCourseAsync(course);   // Update the course entity

            return NoContent();
        }

        // GET: api/StudentCourses/Student/5
        [HttpGet("Student/{studentId}")]
        public async Task<IActionResult> GetStudentCoursesByStudentId(int studentId)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);

            if (student == null) return NotFound();

            var courses = student.StudentCourses;
            return Ok(courses);
        }

        // GET: api/StudentCourses/Course/5
        [HttpGet("Course/{courseName}")]
        public async Task<IActionResult> GetCourseStudentsByCourseId(string courseName)
        {
            var course = await _courseService.GetCourseByNameAsync(courseName);

            if (course == null) return NotFound();

            var students = course.StudentCourses;
            return Ok(students);
        }
    }
}
