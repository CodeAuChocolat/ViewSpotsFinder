using ViewSpots.Models;

namespace ViewSpots
{
  /// <summary>
  /// Ermittelt die ersten N View Spots
  /// </summary>
  /// <seealso cref="ViewSpotFinder"/>
  /// <seealso cref="TrivialViewSpotFinder"/>
  public interface IViewSpotFinder
  {
    IEnumerable<ElementValue> Execute(Mesh mesh, int n = int.MaxValue);
  }
}