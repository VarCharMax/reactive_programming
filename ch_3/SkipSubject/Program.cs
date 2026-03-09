using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SkipSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s14 = new Subject<string>();
    var skip = s14.Skip(2);

    skip.Subscribe(Console.WriteLine);

    s14.OnNext("value1"); //ignored 
    s14.OnNext("value2"); //ignored
    s14.OnNext("value3"); //ok
    s14.OnNext("value4"); //ok

    Console.ReadLine();
  }
}
