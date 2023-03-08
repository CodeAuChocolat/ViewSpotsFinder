using System.Text.Json.Serialization;

namespace ViewSpots.Models
{
  public class Node
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    public override string ToString()
    {
      return $"Node {Id} ({X}, {Y})";
    }
  }
}
