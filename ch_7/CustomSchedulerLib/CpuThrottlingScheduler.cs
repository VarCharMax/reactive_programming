using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Runtime.Versioning;

namespace CustomSchedulerLib;

[SupportedOSPlatform("windows")]
public class CpuThrottlingScheduler : IScheduler, IDisposable
{
  public int CpuLimitPercentage { get; set; } = 80;
  public DateTimeOffset Now { get; private set; }

  private static readonly PerformanceCounter CpuTimeCounter = new("Processor Information", "% Processor Time", "_Total");
  public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
  {
    while (true)
    {
      //checks the CPU time
      var cpu = CpuTimeCounter.NextValue();
      if (cpu >= CpuLimitPercentage)
        Thread.Sleep(200);
      else
        break;
    }

    //once the CPU time is lower than the limit
    //enqueue the job on the thread pool
    new Thread(new ThreadStart(() => action(this, state))).Start();
    Now += TimeSpan.FromTicks(1);

    return Disposable.Create(() => Console.WriteLine($"Job completed at {DateTime.Now} on thread {Environment.CurrentManagedThreadId}"));
  }

  /// <summary>
  /// Not supported! Will be scheduled immediately
  /// </summary>
  public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
  {
    return Schedule<TState>(state, action);
  }

  /// <summary>
  /// Not supported! Will be scheduled immediately
  /// </summary>
  public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
  {
    return Schedule<TState>(state, action);
  }

  public void Dispose()
  {
    CpuTimeCounter.Dispose();
    GC.SuppressFinalize(this);
  }
}
