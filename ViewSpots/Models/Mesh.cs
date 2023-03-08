using System.Text.Json.Serialization;

namespace ViewSpots.Models
{
  public class Mesh
  {
    private IDictionary<int, Node>? _nodesDictionary;
    private IDictionary<int, MeshElement>? _elementsDictionary;
    private IDictionary<int, ElementValue>? _valuesDictionary;

    [JsonPropertyName ("nodes")]
    public IList<Node> Nodes { get; set; } = new List<Node>();

    [JsonPropertyName("elements")]
    public IList<MeshElement> Elements { get; set; } = new List<MeshElement>();

    [JsonPropertyName("values")]
    public IList<ElementValue> Values { get; set; } = new List<ElementValue>();

    public Node GetNode(int nodeId)
    {
      if(_nodesDictionary is null)
      {
        _nodesDictionary = Nodes.ToDictionary(n => n.Id);
      }
      return _nodesDictionary[nodeId];
    }

    public MeshElement GetElement(int elementId)
    {
      if(_elementsDictionary is null)
      {
        _elementsDictionary = Elements.ToDictionary(e => e.Id);
      }
      return _elementsDictionary[elementId];
    }

    public ElementValue GetValue(MeshElement element)
    {
      if(_valuesDictionary is null)
      {
        _valuesDictionary = Values.ToDictionary(v => v.ElementId);
      }
      return _valuesDictionary[element.Id];
    }
  }
}
