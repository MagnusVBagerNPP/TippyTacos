using Newtonsoft.Json;

public class Person : IIdentifiable
{
    [JsonProperty("ID")]
    public string ID { get; set; }

    [JsonProperty("firstName")]
    public required string FirstName { get; set; }

    [JsonProperty("lastName")]
    public required string LastName { get; set; }

    public bool IsDuplicateOf(IIdentifiable other)
    {
        return other is Person t && t.FirstName == FirstName && t.LastName == LastName;
    }
}
