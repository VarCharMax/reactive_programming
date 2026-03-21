using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace EverySomeIncludesSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s17 = new Subject<double>();

    var every = s17.All(x => x > 0); //All greater than 0.
    var some = s17.Any(x => x % 2 == 0); //any exactly divisible by 2.
    var includes = s17.Contains(4d); //Any value equals 4.

    every.Subscribe(x => Console.WriteLine("every: {0}", x));
    some.Subscribe(x => Console.WriteLine("some: {0}", x));
    includes.Subscribe(x => Console.WriteLine("includes: {0}", x));

    //some value
    var r = new Random(DateTime.Now.GetHashCode());
    for (int i = 0; i < 10; i++)
    {
      s17.OnNext(r.NextDouble() * 100d);
    }

    //now operators will flow their message
    s17.OnCompleted();

    Console.ReadLine();
  }
}
