using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DistinctSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s13 = new Subject<string>();
    var distinct = s13.Distinct();
    distinct.Subscribe(Console.WriteLine);

    s13.OnNext("value1");
    s13.OnNext("value2");
    s13.OnNext("value1");
    s13.OnNext("value2");

    Console.ReadLine();
  }
}
