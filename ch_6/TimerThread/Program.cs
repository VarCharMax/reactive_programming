using System.Reactive.Linq;

namespace TimerThread;

internal class Program
{
  static void Main(string[] args)
  {
    var timerBasedSequence = Observable.Interval(TimeSpan.FromSeconds(1))
    .Select(x =>
    {
      Console.WriteLine("Thread Id: {0} -> Yielding new value...", Environment.CurrentManagedThreadId);
      return DateTime.Now;
    });

    timerBasedSequence.Subscribe(x => Console.WriteLine("-> {0}", x));

    Console.ReadLine();
  }
}
