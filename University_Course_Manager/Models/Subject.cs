using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;

public class Subject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string TeacherId { get; set; }

    public string TeacherInfo {get; set; }

public List<string> StudentsEnrolled { get; set; } = new List<string>();


    public Subject(string name, string description, string color, string teacherId)
    {
        Name = name;
        Description = description;
        Color = color;
        TeacherId = teacherId;
    }
    public void EnrollStudent(string studentId)
    {
        if (!StudentsEnrolled.Contains(studentId))
        {
            StudentsEnrolled.Add(studentId);
            Console.WriteLine($"Student {studentId} enrolled in {Name}.");
        }
    }

    public void DropStudent(string studentId)
    {
        if (StudentsEnrolled.Contains(studentId))
        {
            StudentsEnrolled.Remove(studentId);
            Console.WriteLine($"Student {studentId} dropped from {Name}.");
        }
    }
}
