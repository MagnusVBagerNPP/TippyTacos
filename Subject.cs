using Newtonsoft.Json;

public class Subject : IIdentifiable
{
    [JsonProperty("ID")]
    public string ID { get; set; }

    [JsonProperty("Title")]
    public required string Title { get; set; }

    [JsonProperty("TeacherID")]
    public string TeacherID { get; set; }

    [JsonProperty("StudentIDs")]
    public List<string> StudentIDs { get; set; } = new List<string>(); // List of enrolled student IDs

    public bool IsDuplicateOf(IIdentifiable other)
    {
        return other is Subject t && t.Title == Title;
    }
}
