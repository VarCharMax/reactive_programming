using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ElementAtSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s13 = new Subject<string>();
    var indexed = s13.ElementAt(2);

    indexed.Subscribe(Console.WriteLine);

    s13.OnNext("value1"); //ignored
    s13.OnNext("value2"); //ignored
    s13.OnNext("value3"); //OK
    s13.OnNext("value4"); //ignored

    Console.ReadLine();
  }
}
