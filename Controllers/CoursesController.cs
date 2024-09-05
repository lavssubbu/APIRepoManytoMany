using APIRepoPattern.Models;
using APIRepoPattern.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIRepoPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            if (course == null) return BadRequest("Course cannot be null");

            await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.CourseId) return BadRequest("Course ID mismatch");

            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }

}
