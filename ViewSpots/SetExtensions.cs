using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewSpots
{
  internal static class SetExtensions
  {
    public static bool ContainsAny<T>(this ISet<T> set, IEnumerable<T> values)
    {
      foreach(var value in values)
      {
        if(set.Contains(value)) return true;
      }
      return false;
    }

    public static void AddRange<T>(this ISet<T> set, IEnumerable<T> values)
    {
      foreach(var value in values)
      { 
        set.Add(value); 
      }
    }
  }
}
