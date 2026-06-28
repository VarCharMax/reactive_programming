using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ScheduledThread;

internal class Program
{
  static void Main(string[] args)
  {
    var scheduler = Scheduler.Default;

    var loopBasedSequence = Observable.Create<DateTime>(x =>
    {
      int cnt = 0;
      while (true)
      {
        Console.WriteLine("{0} -> Yielding new value...", Environment.CurrentManagedThreadId);
        x.OnNext(DateTime.Now);
        Thread.Sleep(1000);

        if (++cnt == 3)
        {
          x.OnCompleted();
          break;
        }
      }
      return Disposable.Empty;
    });

    loopBasedSequence.SubscribeOn(TaskPoolScheduler.Default).Subscribe(x => Console.WriteLine("{0} -> {1}", Environment.CurrentManagedThreadId, x));
    loopBasedSequence.SubscribeOn(TaskPoolScheduler.Default).Subscribe(x => Console.WriteLine("{0} -> {1}", Environment.CurrentManagedThreadId, x));
    loopBasedSequence.SubscribeOn(TaskPoolScheduler.Default).Subscribe(x => Console.WriteLine("{0} -> {1}", Environment.CurrentManagedThreadId, x));

    Console.ReadLine();
  }
}
