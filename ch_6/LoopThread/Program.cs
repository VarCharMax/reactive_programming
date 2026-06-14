using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace LoopThread;

internal class Program
{
  static void Main(string[] args)
  {
    var loopBasedSequence = Observable.Create<DateTime>(x =>
    {
      int cnt = 0;

      while (true)
      {
        Console.WriteLine("Thread Id: {0} -> Yielding new value...", Environment.CurrentManagedThreadId);
        x.OnNext(DateTime.Now);
        Thread.Sleep(1000);

        if (++cnt == 5)
        {
          x.OnCompleted();
          break;
        }
      }
      return Disposable.Create(() => Console.WriteLine("Killing subscription"));
    });

    loopBasedSequence.Subscribe(x => Console.WriteLine("-> {0}", x));

    Console.ReadLine();
  }
}
