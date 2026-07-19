using System.Reactive.Linq;
using System.Runtime.Versioning;

using CustomSchedulerLib;

namespace CustomScheduler
{
  internal class Program
  {
    [SupportedOSPlatform("windows")]
    static void Main(string[] args)
    {
      using var scheduler = new CpuThrottlingScheduler() { CpuLimitPercentage = 50 };

      //a simple looping sequence
      var sequence = Observable.Range(0, 10, scheduler);

      //a huge observer list
      for (int i = 0; i < 10; i++)
        sequence.Subscribe(x =>
        {
          Thread.SpinWait(100000000);
          Console.WriteLine("Thread: {0} -> value: {1}", Environment.CurrentManagedThreadId, x);
        });

      Console.ReadLine();
    }
  }
}
