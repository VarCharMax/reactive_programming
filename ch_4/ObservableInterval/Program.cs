using System.Reactive.Linq;

namespace ObservableInterval;

internal class Program
{
  static void Main(string[] args)
  {
    //fixed-time interval sequence
    var fixedTimeBasedSequence = Observable.Interval(TimeSpan.FromSeconds(1));

    fixedTimeBasedSequence.Subscribe(ObserverOnNext);

    Console.ReadLine();
  }

  private static void ObserverOnNext(long obj)
  {
    Console.WriteLine("{0} -> {1}", obj, DateTime.Now);
  }
}
