using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewSpots.Models;

namespace ViewSpots.Tests
{
  public class ViewSpotFinderTests
  {
    [Fact]
    public void MeshWithOneElementIsOneViewSpot()
    {
      // Arrange
      var finder = new ViewSpotFinder();
      var mesh = new Mesh
      {
        Nodes = new List<Node>
        {
          new Node { Id = 1, X = 0, Y = 0 },
          new Node { Id = 2, X = 0, Y = 1 },
          new Node { Id = 3, X = 1, Y = 0 }
        },
        Elements = new List<MeshElement>
        {
          new MeshElement { Id = 100, NodeIds = new[] { 1, 2, 3} }
        },
        Values = new List<ElementValue>
        {
          new ElementValue { ElementId = 100, Value = 0 }
        }
      };

      // Act
      var result = finder.Execute(mesh);

      // Assert
      Assert.Single(result);
    }

    [Fact]
    public void MeshWithTwoAdjecentElementsOfEqualHeightIsOneViewSpot()
    {
      // Arrange
      var finder = new ViewSpotFinder();
      var mesh = new Mesh
      {
        Nodes = new List<Node>
        {
          new Node { Id = 1, X = 0, Y = 0 },
          new Node { Id = 2, X = 0, Y = 1 },
          new Node { Id = 3, X = 1, Y = 0 },
          new Node { Id = 4, X = 1, Y = 1 },
        },
        Elements = new List<MeshElement>
        {
          new MeshElement { Id = 100, NodeIds = new[] { 1, 2, 3 } },
          new MeshElement { Id = 101, NodeIds = new[] { 2, 3, 4 } }
        },
        Values = new List<ElementValue>
        {
          new ElementValue { ElementId = 100, Value = 0 },
          new ElementValue { ElementId = 101, Value = 0 }
        }
      };

      // Act
      var result = finder.Execute(mesh);

      // Assert
      Assert.Single(result);
    }

    [Fact]
    public void MeshWithTwoAdjecentElementsOfDifferentHeightIsOneViewSpot()
    {
      // Arrange
      var finder = new ViewSpotFinder();
      var mesh = new Mesh
      {
        Nodes = new List<Node>
        {
          new Node { Id = 1, X = 0, Y = 0 },
          new Node { Id = 2, X = 0, Y = 1 },
          new Node { Id = 3, X = 1, Y = 0 },
          new Node { Id = 4, X = 1, Y = 1 },
          new Node { Id = 5, X = 3, Y = 0 },
        },
        Elements = new List<MeshElement>
        {
          new MeshElement { Id = 100, NodeIds = new[] { 1, 2, 3 } },
          new MeshElement { Id = 101, NodeIds = new[] { 3, 4, 5 } }
        },
        Values = new List<ElementValue>
        {
          new ElementValue { ElementId = 100, Value = 0 },
          new ElementValue { ElementId = 101, Value = 1 }
        }
      };

      // Act
      var result = finder.Execute(mesh);

      // Assert
      Assert.Single(result);
      Assert.Equal(101, result.First().ElementId);
    }

    [Fact]
    public void MeshWithTwoNonAdjecentElementsAreTwoViewSpots()
    {
      // Arrange
      var finder = new ViewSpotFinder();
      var mesh = new Mesh
      {
        Nodes = new List<Node>
        {
          new Node { Id = 1, X = 0, Y = 0 },
          new Node { Id = 2, X = 0, Y = 1 },
          new Node { Id = 3, X = 1, Y = 0 },
          new Node { Id = 4, X = 1, Y = 1 },
          new Node { Id = 5, X = 3, Y = 0 },
          new Node { Id = 6, X = 2, Y = 1 },
        },
        Elements = new List<MeshElement>
        {
          new MeshElement { Id = 100, NodeIds = new[] { 1, 2, 3 } },
          new MeshElement { Id = 101, NodeIds = new[] { 4, 5, 6 } }
        },
        Values = new List<ElementValue>
        {
          new ElementValue { ElementId = 100, Value = 0 },
          new ElementValue { ElementId = 101, Value = 1 }
        }
      };

      // Act
      var result = finder.Execute(mesh).ToList();

      // Assert
      Assert.Equal(2, result.Count);
      Assert.Equal(101, result[0].ElementId);
      Assert.Equal(100, result[1].ElementId);
    }
  }
}
