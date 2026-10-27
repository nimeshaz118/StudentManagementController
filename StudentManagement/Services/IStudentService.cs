using StudentManagement.Models;

namespace StudentManagement.Services;

public interface IStudentService
{
    List<Student> GetAllStudents();

    Student? GetStudentById(string id);

    void AddStudent(Student student);

    bool UpdateStudent(string id, Student student);

    bool DeleteStudent(string id);
}
