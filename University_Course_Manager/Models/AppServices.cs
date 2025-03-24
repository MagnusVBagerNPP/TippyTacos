using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;

public class AppServices
{
    private static AppServices _instance;
    public static AppServices Instance => _instance ??= new AppServices();

    public JsonDatabase<Subject> SubjectsDb { get; }
    public JsonDatabase<Teacher> TeachersDb { get; }
    public JsonDatabase<Student> StudentsDb { get; }
    public UserSession Session { get; set; }

    private AppServices()
    {
        SubjectsDb = new JsonDatabase<Subject>("../Subjects.json");
        TeachersDb = new JsonDatabase<Teacher>("../Teachers.json");
        StudentsDb = new JsonDatabase<Student>("../Students.json");
        Session = new UserSession("0", "0", "0");
    }
}
