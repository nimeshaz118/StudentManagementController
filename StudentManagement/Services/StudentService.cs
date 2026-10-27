using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<Student> _repo;
    private readonly StudentIdGenerator _idGenerator;

    public StudentService(IRepository<Student> repo, StudentIdGenerator idGenerator)
    {
        _repo = repo;
        _idGenerator = idGenerator;
    }

    public List<Student> GetAllStudents() =>
        _repo.GetAll().ToList();

    public Student? GetStudentById(string id) =>
        _repo.GetAll().FirstOrDefault(x => x.StudentId == id);

    public void AddStudent(Student student)
    {
        // Auto-generate StudentId if not provided
        if (string.IsNullOrEmpty(student.StudentId))
        {
            student.StudentId = _idGenerator.GenerateStudentId();
        }

        _repo.Add(student);
        _repo.SaveChanges();
    }

    public bool UpdateStudent(string id, Student student)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return false;

        existing.Name = student.Name;
        existing.Age = student.Age;
        existing.Email = student.Email;
        existing.Courses = student.Courses;

        _repo.Update(existing);
        _repo.SaveChanges();
        return true;
    }

    public bool DeleteStudent(string id)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return false;

        _repo.Delete(existing);
        _repo.SaveChanges();
        return true;
    }
}