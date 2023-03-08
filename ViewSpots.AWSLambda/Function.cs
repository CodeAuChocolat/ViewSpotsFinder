using Amazon.Lambda.Core;
using ViewSpots.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ViewSpots.AWSLambda;

public class Function
{

  /// <summary>
  /// A simple function that takes a string and does a ToUpper
  /// </summary>
  /// <param name="input"></param>
  /// <param name="context"></param>
  /// <returns></returns>
  public IEnumerable<ElementValue>? FunctionHandler(Mesh input, ILambdaContext context)
  {
    try {
      IViewSpotFinder finder = new ViewSpotFinder();
      return finder.Execute(input);
    }
    catch (Exception ex)
    {
      context.Logger.LogError(ex.Message);
      return null;
    }
  }
}
