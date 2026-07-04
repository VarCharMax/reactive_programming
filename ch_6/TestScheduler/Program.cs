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

      Console.ReadLine();
    }
  }
}
