using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace InjectThread;

internal class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Main thread: {0}", Environment.CurrentManagedThreadId);

    //numeric sequence
    var sequence = Observable.Range(1, 10, Scheduler.CurrentThread);

    //observers
    sequence.Subscribe(x => Console.WriteLine("Thread Id:{0} -> value:{1}", Environment.CurrentManagedThreadId, x));
    sequence.Subscribe(x => Console.WriteLine("Thread Id:{0} -> value:{1}", Environment.CurrentManagedThreadId, x));

    Console.ReadLine();
  }
}
