using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
using Newtonsoft.Json;
using University_Course_Manager.Models;

public class JsonDatabase<T>
{
    private readonly string _filePath;
    private List<T> _items;

    public event Action? DataChanged;

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
        Console.WriteLine("Saving file");
        string json = JsonConvert.SerializeObject(_items, Newtonsoft.Json.Formatting.Indented);
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
                if (identifiableItem.Id == null)
                {
                    identifiableItem.Id = GenerateUniqueId();
                    // save the new Person to respective file
                    _items.Add(item);
                    SaveToFile();
                    DataChanged?.Invoke();
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
        JsonDatabase<User> personRepo,
        string Id,
        string subject,
        bool IsStudent,
        string enUn
    )
    {
        string _Id = "Unassigned";
        foreach (var person in personRepo.GetAll())
            if (person.Id == Id)
            {
                subjectRepo.Update(
                    s => s.Name == subject, // lambda expression scanning through all course titles in the database
                    s => // action executed if correct title is found
                    {
                        if (IsStudent)
                        {
                            if (enUn == "Enroll")
                            {
                                s.StudentsEnrolled.Add(Id);
                                Console.WriteLine($"Student {Id} added to {subject}.");
                            }
                            else if (enUn == "!Enroll")
                            {
                                s.StudentsEnrolled.Remove(Id);
                                Console.WriteLine($"Student {Id} removed from {subject}.");
                            }
                        }
                        else
                        {
                            if (enUn == "Enroll")
                            {
                                s.TeacherId = Id;
                                Console.WriteLine($"Teacher {Id} added to {subject}.");
                            }
                            else if (enUn == "!Enroll")
                            {
                                s.TeacherId = null;
                                Console.WriteLine($"Teacher {Id} removed from {subject}.");
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
