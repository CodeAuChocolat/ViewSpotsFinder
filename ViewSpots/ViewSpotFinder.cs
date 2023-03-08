using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewSpots.Models;

namespace ViewSpots
{
  public class ViewSpotFinder : IViewSpotFinder
  {
    /// <summary>
    /// Diese Implementation durchläuft die Element vom höchsten zum niedrigsten und
    /// merkt sich für jedes Element die Vertices der besuchten Elemente.
    /// Wurde ein Vertex des Elements bereits besucht, dann muss es einen direkten
    /// Nachbarn geben mit mindestens der gleichen Höhe geben. Dann kann dieses
    /// Element kein View Spot sein. Ansonsten ist es ein View Spot.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public IEnumerable<ElementValue> Execute(Mesh mesh, int n = int.MaxValue)
    {
      var orderedValues = mesh.Values.OrderByDescending(v => v.Value);
      HashSet<int> visitedVertices = new();
      List<ElementValue> resultValues = new();

      foreach (var value in orderedValues)
      {
        var element = mesh.GetElement(value.ElementId);
        var vertices = element.NodeIds;
        var visited = visitedVertices.ContainsAny(vertices);
        if (visited == false)
        {
          resultValues.Add(value);
          if(resultValues.Count == n) break;
        }
        visitedVertices.AddRange(vertices);
      }

      return resultValues;
    }
  }
}
