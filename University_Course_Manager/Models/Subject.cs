using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public int TeacherId { get; set; }

    public string TeacherInfo {get; set; }

public List<int> StudentsEnrolled { get; set; } = new List<int>();


    public Subject(string name, string description, string color, int teacherId)
    {
        Name = name;
        Description = description;
        Color = color;
        TeacherId = teacherId;
    }
    public void EnrollStudent(int studentId)
    {
        if (!StudentsEnrolled.Contains(studentId))
        {
            StudentsEnrolled.Add(studentId);
            Console.WriteLine($"Student {studentId} enrolled in {Name}.");
        }
    }

    public void DropStudent(int studentId)
    {
        if (StudentsEnrolled.Contains(studentId))
        {
            StudentsEnrolled.Remove(studentId);
            Console.WriteLine($"Student {studentId} dropped from {Name}.");
        }
    }
}
