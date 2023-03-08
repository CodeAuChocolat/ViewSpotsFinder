using System.Text.Json;
using ViewSpots.Models;

namespace ViewSpots.Cli;

internal class Program
{
  static async Task Main(string[] args)
  {
    try
    {
      string file = args[0];
      int n = int.Parse(args[1]);
      string implementation = args.Length > 2 ? args[2] : "";

      Mesh mesh = await ReadMeshFileAsync(file);

      IViewSpotFinder viewSpotFinder = GetFinder(implementation);
      IEnumerable<ElementValue> viewSpots = viewSpotFinder.Execute(mesh, n);

      WriteOutput(viewSpots);
    }
    catch(Exception ex)
    {
      Console.Error.WriteLine(ex.Message);
    }
  }

  public static IViewSpotFinder GetFinder(string? i)
  {
    return i == "trivial" ? new TrivialViewSpotFinder() : new ViewSpotFinder();
  }

  static async Task<Mesh> ReadMeshFileAsync(string path)
  {
    using var stream = new FileStream(path, FileMode.Open);
    return await ReadMeshAsync(stream);
  }

  static async Task<Mesh> ReadMeshAsync(Stream stream)
  {
    Mesh? mesh = await JsonSerializer.DeserializeAsync<Mesh>(stream);
    if(mesh is null) throw new ArgumentException("Invalid mesh file.");
    return mesh;
  }

  static void WriteOutput(IEnumerable<ElementValue> viewSpots)
  {
    var options = new JsonSerializerOptions
    {
      WriteIndented = true
    };
    var json = JsonSerializer.Serialize(viewSpots, options);
    Console.Write(json);
  }
}