using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;

public class Student : User
{
    public List<int> EnrolledSubjects { get; set; } = new List<int>();

    public void EnrollStudent(int subjectId)
    {
        if (!EnrolledSubjects.Contains(subjectId))
        {
            EnrolledSubjects.Add(subjectId);
            Console.WriteLine($"Student {Name} enrolled in subject {subjectId}.");
        }
    }

    public void DropStudent(int subjectId)
    {
        if (EnrolledSubjects.Contains(subjectId))
        {
            EnrolledSubjects.Remove(subjectId);
            Console.WriteLine($"Student {Name} dropped subject {subjectId}.");
        }
    }
}
