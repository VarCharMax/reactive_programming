using System.Reactive.Concurrency;

using Microsoft.Reactive.Testing;

namespace AdvanceScheduler;

internal class Program
{
  static void Main(string[] args)
  {
    var scheduler = new TestScheduler();

    //schedule a future job at 1 minute
    scheduler.Schedule(TimeSpan.FromMinutes(1), () => Console.WriteLine("1 minute now"));
    Console.WriteLine("scheduler.Now -> {0}", scheduler.Now);

    //advance to 00:00:30
    scheduler.AdvanceTo(scheduler.Clock + TimeSpan.FromSeconds(30).Ticks);
    Console.WriteLine("scheduler.Now -> {0}", scheduler.Now);

    //advance to 00:01:00
    scheduler.AdvanceTo(scheduler.Clock + TimeSpan.FromSeconds(60).Ticks);

    Console.WriteLine("scheduler.Now -> {0}", scheduler.Now);
    scheduler.Start();

    Console.ReadLine();
  }
}
