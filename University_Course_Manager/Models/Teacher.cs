using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;
public class Teacher : User
{
    public List<int> TaughtSubjects { get; set; } = new List<int>();
}
