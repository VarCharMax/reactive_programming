using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ConcatSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s8 = new Subject<string>();
    var s9 = new Subject<string>();

    var concat = s8.Concat(s9);
    concat.Subscribe(Console.WriteLine);

    //some message
    s8.OnNext("s1 - value1");
    s8.OnNext("s1 - value2");
    s9.OnNext("s2 - value1"); //missed
    s9.OnNext("s2 - value2"); //missed
    s8.OnNext("s1 - value3");

    //close first sequence
    s8.OnCompleted();

    //only now messages from second sequence will start flowing
    s9.OnNext("s2 - value3");

    Console.ReadLine();
  }
}
