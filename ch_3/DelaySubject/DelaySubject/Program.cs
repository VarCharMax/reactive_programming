using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DelaySubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s3 = new Subject<string>();
    var delay = s3.Delay(TimeSpan.FromSeconds(5));

    delay.Subscribe(Console.WriteLine);

    Console.WriteLine("Emitting values with 5 sec delay ...");
    s3.OnNext("value1");
    s3.OnNext("value2");

    Console.ReadLine();
  }
}
