using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
