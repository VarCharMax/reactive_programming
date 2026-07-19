using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace StateScheduler;

internal class Program
{
  static void Main(string[] args)
  {
    int value = 0;

    //a scheduler
    var scheduler = Scheduler.Default;

    //multiple immediate jobs
    scheduler.Schedule(x => value = 14);
    scheduler.Schedule(x => Console.WriteLine(value));
    scheduler.Schedule(x => value = 15);
    scheduler.Schedule(x => Console.WriteLine(value));
    scheduler.Schedule(x => value = 16);
    scheduler.Schedule(x => Console.WriteLine(value));

    Console.ReadLine();

    value = 14;
    scheduler.Schedule<int>(value, (scheduler, state) =>
    {
      Console.WriteLine(state);
      return Disposable.Empty;
    });

    value = 15;
    scheduler.Schedule<int>(value, (scheduler, state) =>
    {
      Console.WriteLine(state);
      return Disposable.Empty;
    });

    value = 16;
    scheduler.Schedule<int>(value, (scheduler, state) =>
    {
      Console.WriteLine(state);
      return Disposable.Empty;
    });

    Console.ReadLine();
  }
}

