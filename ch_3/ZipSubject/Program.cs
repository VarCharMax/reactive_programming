using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ZipSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s11 = new Subject<string>();
    var s12 = new Subject<double>();

    var zip = s11.Zip(s12, (x, y) => new { text = x, value = y });
    zip.Subscribe(x => Console.WriteLine("{0}: {1}", x.text, x.value));

    //same example of combine latest
    s11.OnNext("Mr. Brown");
    s12.OnNext(10);
    s12.OnNext(20);
    s12.OnNext(30);
    s12.OnNext(40);
    s11.OnNext("Mr. Green");
    s11.OnNext("Mr. White");
    s12.OnNext(50);
    s11.OnNext("Mr. Black");
    s11.OnNext("Mr. Red");
    //this time the output is synchronized

    Console.ReadLine();
  }
}

