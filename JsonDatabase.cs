using Newtonsoft.Json;

public class JsonDatabase<T>
{
    private readonly string _filePath;
    private List<T> _items;

    public static string GenerateUniqueId()
    {
        return Guid.NewGuid().ToString(); // Generates a unique ID (UUID)
    }

    public JsonDatabase(string filePath)
    {
        _filePath = filePath;
        _items = LoadFromFile();
    }

    // Load data from JSON
    private List<T> LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return new List<T>();

        string json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
    }

    // Save data to JSON
    private void SaveToFile()
    {
        string json = JsonConvert.SerializeObject(_items, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    // Create (Add)
    public void Add(T item)
    {
        //Check if passed item is created using the IIdentifiable class
        if (item is IIdentifiable identifiableItem)
        {
            // Check for duplicates
            if (
                !_items
                    .OfType<IIdentifiable>()
                    .Any(existing => existing.IsDuplicateOf(identifiableItem))
            )
            {
                // Assign a new ID only if it doesn't exist
                if (identifiableItem.ID == null)
                {
                    identifiableItem.ID = GenerateUniqueId();
                    // save the new Person to respective file
                    _items.Add(item);
                    SaveToFile();
                }
            }
            else
                Console.WriteLine("This item is already in the list.");
        }
    }

    // Reads every first level object of List and respective properties
    public List<T> GetAll() => _items;

    // enrollment and unenrollment of students
    public void enrollment_unenrollment(
        JsonDatabase<Subject> subjectRepo,
        JsonDatabase<Person> personRepo,
        string FirstName,
        string LastName,
        string subject,
        bool IsStudent,
        string enUn
    )
    {
        string ID = "Unassigned";
        foreach (var person in personRepo.GetAll())
            if (person.FirstName == FirstName && person.LastName == LastName)
            {
                ID = person.ID;
                subjectRepo.Update(
                    s => s.Title == subject, // lambda expression scanning through all course titles in the database
                    s => // action executed if correct title is found
                    {
                        if (IsStudent)
                        {
                            if (enUn == "Enroll")
                            {
                                s.StudentIDs.Add(ID);
                                Console.WriteLine($"Student {ID} added to {subject}.");
                            }
                            else if (enUn == "!Enroll")
                            {
                                s.StudentIDs.Remove(ID);
                                Console.WriteLine($"Student {ID} removed from {subject}.");
                            }
                        }
                        else
                        {
                            if (enUn == "Enroll")
                            {
                                s.TeacherID = ID;
                                Console.WriteLine($"Teacher {ID} added to {subject}.");
                            }
                            else if (enUn == "!Enroll")
                            {
                                s.TeacherID = null;
                                Console.WriteLine($"Teacher {ID} removed from {subject}.");
                            }
                        }
                    }
                );
            }
            else
            {
                Console.WriteLine("Person not found");
            }
    }

    // Update, gives functionality to manually overwrite all individual contents
    // usage should be reserved for admin account
    public bool Update(Predicate<T> predicate, Action<T> updateAction)
    {
        var item = _items.Find(predicate); // Finds the first matching item
        if (item != null)
        {
            updateAction(item); // Applies the update
            SaveToFile();
            return true;
        }
        else
        {
            Console.WriteLine("Item does not exist, no changes have been made");
        }
        return false;
    }

    // Delete objects from a repo, can be used to delete students, teachers and whole courses(including enrolled teachers and students)
    public bool Delete(Predicate<T> predicate)
    {
        int removedCount = _items.RemoveAll(predicate); // Remove all matching items
        if (removedCount > 0)
        {
            SaveToFile();
            return true;
        }
        return false;
    }
}
