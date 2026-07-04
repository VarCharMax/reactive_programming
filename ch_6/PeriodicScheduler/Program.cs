using System.Reactive.Concurrency;

using Microsoft.Reactive.Testing;

namespace PeriodicScheduler;

internal class Program
{
  static void Main(string[] args)
  {
    //a scheduler for testing purposes
    var scheduler = new TestScheduler();

    //schedule a periodic job and output the virtual time
    scheduler.SchedulePeriodic(TimeSpan.FromSeconds(1), () => Console.WriteLine("{0} -> Periodic", scheduler.Now));

    //this would produce an infinite output
    //scheduler.Start();

    //to avoid the infinite output, we will need to schedule a Stop request
    scheduler.Schedule(TimeSpan.FromSeconds(30), () => scheduler.Stop());

    //play the whole record
    scheduler.Start();

    //append immediately
    scheduler.Schedule(TimeSpan.FromTicks(1), () => Console.WriteLine("Running again"));

    //schedule another Stop
    scheduler.Schedule(TimeSpan.FromSeconds(30), () => scheduler.Stop());

    //start the scheduler again
    scheduler.Start();


    Console.ReadLine();
  }
}
