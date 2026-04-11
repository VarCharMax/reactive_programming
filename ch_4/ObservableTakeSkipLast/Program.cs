using System.Reactive.Linq;

namespace ObservableTakeSkipLast;

internal class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Starting: {0}", DateTime.Now);

    //a sourcing sequence for a time-window of 5 seconds
    var sourcingSequence = Observable.Interval(TimeSpan.FromSeconds(1))
      .Select(id => DateTime.UtcNow)
      .TakeUntil(DateTimeOffset.Now.AddSeconds(10));

    //skip last messages within a time-window of 3 seconds
    var skipLast = sourcingSequence.SkipLast(TimeSpan.FromSeconds(3));
    skipLast.Subscribe(value => Console.WriteLine("SkipLast: {0}", value));

    //take last messages within a time-window of 3 seconds
    var takeLast = sourcingSequence.TakeLast(TimeSpan.FromSeconds(3));
    takeLast.Subscribe(value => Console.WriteLine("TakeLast: {0}", value));

    Console.ReadLine();
  }
}
