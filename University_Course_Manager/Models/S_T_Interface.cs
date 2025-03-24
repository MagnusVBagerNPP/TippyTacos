using Newtonsoft.Json;

public interface IIdentifiable
{
    string Id { get; set; }

    bool IsDuplicateOf(IIdentifiable other);
}
