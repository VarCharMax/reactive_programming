using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ObserveOnThread;

internal class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Main thread: {0}", Environment.CurrentManagedThreadId);

    var sequence = Observable.Create<DateTime>(x =>
    {
      //let take some time before registering the new observer
      for (int i = 0; i < 10; i++)
      {
        Console.WriteLine("Registering observer on thread {0}...", Environment.CurrentManagedThreadId);
        Thread.Sleep(100);
      }

      //produce 5 messages
      for (int i = 0; i < 5; i++)
      {
        x.OnNext(DateTime.Now);
        Thread.Sleep(100);
      }

      //exit
      return Disposable.Empty;
    });

    //register two subscribers
    //sequence.SubscribeOn(Scheduler.Default).Subscribe(x => Console.WriteLine("Subscribed on {0} -> {1}", Environment.CurrentManagedThreadId, x));
    //sequence.SubscribeOn(Scheduler.Default).Subscribe(x => Console.WriteLine("Subscribed on {0} -> {1}", Environment.CurrentManagedThreadId, x));

    //register two observers
    sequence.ObserveOn(Scheduler.Default).Subscribe(x => Console.WriteLine("Observing on {0} -> {1}", Environment.CurrentManagedThreadId, x));
    sequence.ObserveOn(Scheduler.Default).Subscribe(x => Console.WriteLine("Observing on {0} -> {1}", Environment.CurrentManagedThreadId, x));

    Console.ReadLine();
  }
}
