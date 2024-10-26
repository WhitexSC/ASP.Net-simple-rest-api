using System.Text.Json.Serialization;

namespace TestAPI;

// public record Repository(string description, string name, [property: JsonPropertyName("id")] int Id);
public record Repository(
  [property: JsonPropertyName("name")] string Name,
  [property: JsonPropertyName("description")]
  string Description,
  [property: JsonPropertyName("id")] int Id,
  [property: JsonPropertyName("pushed_at")]
  DateTime LastPushUtc)
{
  public DateTime LastPush => LastPushUtc.ToLocalTime();
}