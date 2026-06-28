using System.Reactive.Concurrency;

namespace CustomScheduler;

internal class Program
{
  static void Main(string[] args)
  {
    using (var job1 = Scheduler.Default.Schedule(OnJob1Executed))
      //job timeout 
      Thread.Sleep(2000);

    Console.WriteLine("END");
    Console.ReadLine();

  }

  static void OnJob1Executed()
  {
    for (int i = 0; i < 10; i++)
    {
      Console.Write(".");
      Thread.Sleep(100);
    }

    Console.WriteLine();
    Console.WriteLine("JOB END");
  }
}