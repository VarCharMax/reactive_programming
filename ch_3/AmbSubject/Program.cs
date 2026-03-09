using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AmbSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s20 = new Subject<string>();
    var s21 = new Subject<string>();

    var amb = s20.Amb(s21);
    amb.Subscribe(Console.WriteLine);

    //the first message will let amb operator
    //choose the definite source sequence

    s21.OnNext("s2 - value1");
    //messages from the other sequences are ignored
    s20.OnNext("s1 - value1");
    s20.OnNext("s1 - value2");
    s20.OnNext("s1 - value3");
    s20.OnNext("s1 - value4");
    s20.OnNext("s1 - value5");

    s21.OnNext("s2 - value2");
    s21.OnNext("s2 - value3");
    s21.OnNext("s2 - value4");
    s21.OnNext("s2 - value5");

    Console.ReadLine();
  }
}
