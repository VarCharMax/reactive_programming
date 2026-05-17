using System.Reactive.Linq;

namespace DebugDo;

internal class Program
{
  static void Main(string[] args)
  {
    var source = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(x => DateTime.Now)
        .Take(10)
        .Select(static x => x.Second % 10 == 0 ? throw new ArgumentException() : x)
        .Do(OnNext, OnError, OnCompleted)
        .Catch(Observable.Empty<DateTime>());

    //starts the source
    source.Subscribe();

    Console.ReadLine();
  }
  private static void OnError(Exception ex)
  {
    Console.WriteLine("-> {0}", ex.Message);
  }

  private static void OnCompleted()
  {
    Console.WriteLine("-> END");
  }

  private static void OnNext(DateTime obj)
  {
    Thread.Sleep(3000);
    Console.WriteLine("-> {0}", obj);
  }
}

