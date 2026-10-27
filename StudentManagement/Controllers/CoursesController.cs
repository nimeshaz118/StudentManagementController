using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Courses.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);

        if (course == null)
            return NotFound();

        return Ok(course);
    }

    [HttpPost]
    public IActionResult Add(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();

        return Ok(course);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Course course)
    {
        var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseId == id);

        if (existingCourse == null)
            return NotFound();

        existingCourse.CourseName = course.CourseName;
        _context.SaveChanges();

        return Ok(existingCourse);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);

        if (course == null)
            return NotFound();

        _context.Courses.Remove(course);
        _context.SaveChanges();

        return Ok("Course deleted");
    }
}
