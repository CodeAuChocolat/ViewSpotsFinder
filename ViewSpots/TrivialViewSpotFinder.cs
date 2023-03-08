using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewSpots.Models;

namespace ViewSpots
{
  /// <summary>
  /// Diese Implementation läuft durch alle Element durch, ermittelt für jedes
  /// Element die Nachbarn und prüft, ob die Nachbarn nicht höher liegen.
  /// <seealso cref="ViewSpotFinder"/> für eine bessere Implementation.
  /// </summary>
  public class TrivialViewSpotFinder : IViewSpotFinder
  {
    public IEnumerable<ElementValue> Execute(Mesh mesh, int n = int.MaxValue)
    {
      List<ElementValue> viewSpots = new List<ElementValue>();
      foreach(var element in mesh.Elements)
      {
        if(IsViewSpot(mesh, element, viewSpots))
        {
          var value = mesh.GetValue(element);
          viewSpots.Add(value);
        }
      }

      return viewSpots
        .OrderByDescending(spot => spot.Value)
        .Take(n);
    }

    private bool IsViewSpot(Mesh mesh, MeshElement element, List<ElementValue> viewSpots)
    {
      var value = mesh.GetValue(element);
      IEnumerable<MeshElement> neighbours = GetNeighbours(mesh, element);
      return neighbours.All(n => mesh.GetValue(n).Value <= value.Value)
        && neighbours.Any(n => viewSpots.Any(vs => vs.ElementId == n.Id)) == false;
    }

    private IEnumerable<MeshElement> GetNeighbours(Mesh mesh, MeshElement element)
    {
      List<MeshElement> neighbours = new List<MeshElement>();
      foreach(var vertex in element.NodeIds)
      {
        var vertexNeighbours = mesh.Elements
          .Where(e => e != element)
          .Where(e => e.NodeIds.Contains(vertex));

        neighbours.AddRange(vertexNeighbours);
      }
      return neighbours.Distinct();
    }
  }
}
