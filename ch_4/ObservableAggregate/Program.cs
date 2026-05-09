using System.Reactive.Linq;

namespace ObservableAggregate;

internal class Program
{
  static void Main(string[] args)
  {
    //a sourcing sequence of random doubles
    var sourcingSequence = Observable.Create<double>(observer =>
    {
      var r = new Random(DateTime.Now.GetHashCode());

      for (int i = 0; i < 5; i++)
      {
        observer.OnNext((r.NextDouble() - 0.5d) * 10d);
        Thread.Sleep(1000);
      }

      observer.OnCompleted();

      return new Action(() => Console.WriteLine("Completed"));
    });

    //aggregate values to compute a single ending value
    var aggregationSequence = sourcingSequence.Aggregate(0d, (rolling, value) => //Previous result from this function and current value from OnNext event.
    {
      Console.WriteLine("Aggregating: {0} + {1} = {2}", rolling, value, rolling + value);

      return rolling + value;
    });

    aggregationSequence.Subscribe(value => Console.WriteLine("Aggregated value: {0}", value));

    Console.ReadLine();
  }
}
