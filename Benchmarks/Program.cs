using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.IO;
using System.Text.Json;
using ViewSpots;
using ViewSpots.Models;

namespace Benchmarks
{
  public class Program
  {
    public class ViewSpotFinderBenchmarks
    {
      private readonly Mesh _mesh;

      public ViewSpotFinderBenchmarks()
      {
        using var stream = new FileStream(@"C:\Temp\mesh\mesh_x_sin_cos_10000.json", FileMode.Open);
        _mesh = JsonSerializer.Deserialize<Mesh>(stream)!;
      }

      [Benchmark]
      public List<ElementValue> DefaultImplementation10000()
      {
        var finder = new ViewSpotFinder();
        return finder.Execute(_mesh).ToList();
      }

      [Benchmark]
      public List<ElementValue> TrivialImplementation10000()
      {
        var finder = new TrivialViewSpotFinder();
        return finder.Execute(_mesh).ToList();
      }
    }

    public static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<ViewSpotFinderBenchmarks>();
    }
  }
}