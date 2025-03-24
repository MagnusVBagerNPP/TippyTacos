using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        var studentRepo = new JsonDatabase<Person>("Students.json");
        var teacherRepo = new JsonDatabase<Person>("Teachers.json");
        var subjectRepo = new JsonDatabase<Subject>("Subjects.json");

        // create a new student or teacher
        studentRepo.Add(new Person { FirstName = "Jonas", LastName = "Wild" }); //
        studentRepo.Add(new Person { FirstName = "Hanna", LastName = "Schuster" }); //
        teacherRepo.Add(new Person { FirstName = "Kalter", LastName = "Bär" }); //
        // create a new subject
        subjectRepo.Add(new Subject { Title = "Science" });
        subjectRepo.enrollment_unenrollment( // enroll student in new subject
            subjectRepo,
            studentRepo,
            FirstName: "Jonas",
            LastName: "Wild",
            subject: "Science",
            IsStudent: true,
            enUn: "Enroll"
        );
        subjectRepo.enrollment_unenrollment( // enroll student in new subject
            subjectRepo,
            studentRepo,
            FirstName: "Hanna",
            LastName: "Schuster",
            subject: "Science",
            IsStudent: true,
            enUn: "Enroll"
        );
        subjectRepo.enrollment_unenrollment(
            subjectRepo,
            studentRepo,
            FirstName: "Magnus",
            LastName: "Bager",
            subject: "Science",
            IsStudent: false,
            enUn: "Enroll"
        );


        //subjectRepo.Update(s => s.Title == "Math2", s => s.TeacherID = "Hans1234");
        //subjectRepo.Update(s => s.Title == "Math2", s => s.StudentIDs.Add(studentID));

        //subjectRepo.Update(s => s.Title == "Math2", s => s.TeacherID = "1234");
        //subjectRepo.enroll_student()


        foreach (var student in studentRepo.GetAll())
            Console.WriteLine($"{student.FirstName} {student.LastName}");

        //foreach (var student in studentRepo.GetAll())
        //    Console.WriteLine($"{student.FirstName} {student.LastName}");
        //Console.WriteLine("----");
        // Add a teacher
        //teacherRepo.Add(new Person { FirstName = "Felix", LastName = "Hoch" }); // Read teachers
        //foreach (var teacher in teacherRepo.GetAll())
        //    Console.WriteLine($"{teacher.FirstName} {teacher.LastName}");

        // Update a Student
        //studentRepo.Update(s => s.FirstName == "Hans", s => s.LastName = "Müller");

        // Delete a Student
        //studentRepo.Delete(s => s.FirstName == "Hans" && s.LastName == "Peter");

        // Add a Teacher
        // teacherRepo.Add(new Teacher { FirstName = "John", LastName = "Doe"});

        // Read Teachers
        //foreach (var teacher in teacherRepo.GetAll())
        //    Console.WriteLine($"{teacher.FirstName} {teacher.LastName}");

        // read subject with teachers and students
    }
}
