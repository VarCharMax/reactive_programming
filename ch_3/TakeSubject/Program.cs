using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TakeSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s15 = new Subject<string>();
    var take = s15.Take(2);

    take.Subscribe(Console.WriteLine);

    s15.OnNext("value1"); //ok 
    s15.OnNext("value2"); //ok
    s15.OnNext("value3"); //ignored
    s15.OnNext("value4"); //ignored

    Console.ReadLine();
  }
}
