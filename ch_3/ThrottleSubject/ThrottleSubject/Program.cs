using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ThrottleSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s5 = new Subject<DateTime>();
    var throttle = s5.Throttle(TimeSpan.FromMilliseconds(500));
    throttle.Subscribe(x => Console.WriteLine("{0:T}", x));

    Thread newThread = new(() => Run(s5));
    newThread.Start();

    Console.ReadLine();
  }

  private static void Run(Subject<DateTime> s)
  {
    //produce 100 messages
    for (int i = 0; i < 1000; i++)
    {
      s.OnNext(DateTime.Now);
      Thread.Sleep(100);
    }
  }
}
