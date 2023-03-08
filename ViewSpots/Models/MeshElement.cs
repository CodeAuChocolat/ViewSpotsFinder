using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ViewSpots.Models
{
  public class MeshElement
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nodes")]
    public int[] NodeIds { get; set; } = new int[0];

    public override string ToString()
    {
      return $"Element {Id}";
    }
  }
}
