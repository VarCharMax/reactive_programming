using System.Reactive.Concurrency;

namespace TestScheduler
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //a scheduler for testing purposes
      var scheduler = new Microsoft.Reactive.Testing.TestScheduler();

      //output the virtual clock
      Console.WriteLine("scheduler.Now -> {0}", scheduler.Now);

      //schedule a future job
      scheduler.Schedule(TimeSpan.FromDays(22), () => Console.WriteLine("22 days now"));
      Console.WriteLine("scheduler.Now -> {0}", scheduler.Now);

      //play the recorded scheduled jobs at normal speed
      scheduler.Start();

      Console.WriteLine("scheduler.Now -> {0}", scheduler.Now);

      //schedule a future job at 1 minute
      scheduler.Schedule(TimeSpan.FromMinutes(1), () => Console.WriteLine("1 minute now"));
      Console.WriteLine("-> {0}", scheduler.Now);

      //advance to 00:00:30
      scheduler.AdvanceTo(scheduler.Clock + TimeSpan.FromSeconds(30).Ticks);
      Console.WriteLine("-> {0}", scheduler.Now);

      //advance to 00:01:00
      scheduler.AdvanceTo(scheduler.Clock + TimeSpan.FromSeconds(60).Ticks);
      Console.WriteLine("-> {0}", scheduler.Now);

      //schedule a periodic job and output the virtual time
      //scheduler.SchedulePeriodic(TimeSpan.FromSeconds(1), () => Console.WriteLine("{0} -> Periodic", scheduler.Now));

      //this would produce an infinite output
      //scheduler.Start();

      //to avoid the infinite output, we will need to schedule a Stop request
      //scheduler.Schedule(TimeSpan.FromSeconds(60), () => scheduler.Stop());

      //play the whole record
      //scheduler.Start();

      //append immediately
      //scheduler.Schedule(TimeSpan.FromTicks(1), () => Console.WriteLine("Running again"));

      //schedule another Stop
      //scheduler.Schedule(TimeSpan.FromSeconds(60), () => scheduler.Stop());

      //start again the scheduler
      scheduler.Start();

      Console.ReadLine();
    }
  }
}
