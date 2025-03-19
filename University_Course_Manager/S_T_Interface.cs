using Newtonsoft.Json;

public interface IIdentifiable
{
    string ID { get; set; }

    bool IsDuplicateOf(IIdentifiable other);
}
