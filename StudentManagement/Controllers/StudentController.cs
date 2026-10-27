using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAllStudents());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var student = _service.GetStudentById(id);

        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        _service.AddStudent(student);
        return Ok(student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, Student student)
    {
        var result = _service.UpdateStudent(id, student);

        if (!result)
            return NotFound();

        return Ok(student);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var result = _service.DeleteStudent(id);

        if (!result)
            return NotFound();

        return Ok("Student deleted");
    }
}