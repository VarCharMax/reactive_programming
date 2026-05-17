using System.Reactive.Linq;

namespace DebugAny
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var r = new Random(DateTime.Now.GetHashCode());

      //an infinite message source of integer numbers
      //running at 10hz
      var source = Observable.Interval(TimeSpan.FromMilliseconds(1000))
          .Select(x => r.Next(1, 20));

      //some console output
      source.Subscribe(x => Console.WriteLine(x));

      //we want message metadata             
      var any = source.Any(x => x == 10).Materialize();

      any.Subscribe(x => Console.WriteLine("FOUND ANY: {0}", x));

      Console.ReadLine();
    }
  }
}
