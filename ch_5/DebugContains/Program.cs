using System.Reactive.Linq;

namespace DebugContains;

internal class Program
{
  static void Main(string[] args)
  {
    var r = new Random(DateTime.Now.GetHashCode());

    //an infinite message source of integer numbers
    //running at 10hz
    var source = Observable.Interval(TimeSpan.FromMilliseconds(1000))
        .Select(x => r.Next(1, 20));

    //we want message metadata
    var contains = source.Contains(10).Materialize();

    //some console output
    source.Subscribe(x => Console.WriteLine(x));
    contains.Subscribe(x => Console.WriteLine("FOUND CONTAINS: {0}", x));

    Console.ReadLine();
  }
}
