using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewSpots.Models
{
  public class ElementValue
  {
    [JsonPropertyName("element_id")]
    public int ElementId { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }

    public override string ToString()
    {
      return $"Value for {ElementId}: {Value}";
    }
  }
}
