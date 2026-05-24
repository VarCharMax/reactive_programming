using System.Reactive.Linq;

namespace DebugRetry;

internal class Program
{
  static void Main(string[] args)
  {
    //a finite sequence of 10 values
    var source = Observable.Interval(TimeSpan.FromSeconds(1))
        .Take(10)
        .Select(x => DateTime.Now)
        .Select(x =>
          //lets raise some error
          x.Second % 5 == 0 ? throw new ArgumentException("Wrong milliseconds value") : x)
        //restart he sourcing sequence on error (max 3 times)
        .Retry(3)
        //materialize to read message metadata
        .Materialize();

    source.Subscribe(x => Console.WriteLine(x));
    Console.ReadLine();
  }
}
