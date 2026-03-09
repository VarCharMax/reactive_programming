using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace StartWithSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s10 = new Subject<string>();
    var swith = s10.StartWith("value0"); //Sequence will start with value0, then will continue with values from s10

    swith.Subscribe(Console.WriteLine);

    s10.OnNext("value1");
    s10.OnNext("value2");

    Console.ReadLine();
  }
}
