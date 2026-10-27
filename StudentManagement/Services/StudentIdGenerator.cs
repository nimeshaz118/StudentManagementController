using System.Linq;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Services;

public class StudentIdGenerator
{
    private readonly IRepository<Student> _studentRepo;

    public StudentIdGenerator(IRepository<Student> studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public string GenerateStudentId()
    {
        // Get the last student ordered by StudentId
        var lastStudent = _studentRepo.GetAll()
            .OrderByDescending(s => s.StudentId)
            .FirstOrDefault();

        int nextNumber = 1;

        if (lastStudent != null && lastStudent.StudentId.StartsWith("stu"))
        {
            // Extract the numeric part (e.g., "0001" from "stu0001")
            if (int.TryParse(lastStudent.StudentId.Substring(3), out int lastNum))
            {
                nextNumber = lastNum + 1;
            }
        }

        // Format as stu0001, stu0002, etc.
        return $"stu{nextNumber:D4}";
    }
}
