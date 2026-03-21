using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MathSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s16 = new Subject<double>();

    var min = s16.Min(); //register for finding the min
    var max = s16.Max(); //register for finding the max
    var avg = s16.Average(); //register for finding the average
    var sum = s16.Sum(); //register for finding the count
    var count = s16.Count(); //register for finding the sum

    min.Subscribe(x => Console.WriteLine("min: {0}", x));
    max.Subscribe(x => Console.WriteLine("max: {0}", x));
    avg.Subscribe(x => Console.WriteLine("avg: {0}", x));
    sum.Subscribe(x => Console.WriteLine("sum: {0}", x));
    count.Subscribe(x => Console.WriteLine("count: {0}", x));

    //some value
    var r = new Random(DateTime.Now.GetHashCode());

    for (int i = 0; i < 10; i++)
    {
      s16.OnNext(r.NextDouble() * 100d);
    }

    //now aggregation operators will flow their message
    s16.OnCompleted();

    Console.ReadLine();
  }
}
