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
  private readonly List<CancellationTokenSource> _cancellationTokens = [];

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

    var cts = new CancellationTokenSource();
    _cancellationTokens.Add(cts);
    var token = cts.Token;

    //once the CPU time is lower than the limit
    //enqueue the job on the thread pool
    var thread = new Thread(() =>
    {
      if (!token.IsCancellationRequested)
      {
        action(this, state);
      }
    });
    thread.Start();
    Now += TimeSpan.FromTicks(1);

    return Disposable.Create(() =>
    {
      cts.Cancel();
      thread.Join(500); // Wait for thread to finish, up to 500ms
      Console.WriteLine($"Job completed at {DateTime.Now} on thread {Environment.CurrentManagedThreadId}");
      _cancellationTokens.Remove(cts);
      cts.Dispose();
    });
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
    foreach (var cts in _cancellationTokens.ToArray())
    {
      cts.Cancel();
      cts.Dispose();
    }
    _cancellationTokens.Clear();
    CpuTimeCounter.Dispose();
    GC.SuppressFinalize(this);
  }
}
