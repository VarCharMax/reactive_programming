using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MapSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s4 = new Subject<string>();

    //a numeric sequence
    var map = s4.Select(x => double.Parse(x) * 5);
    map.Subscribe(x => Console.WriteLine("{0:N4}", x));

    s4.OnNext("10.40");
    s4.OnNext("12.55");

    Console.ReadLine();
  }
}
