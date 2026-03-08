using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MergeSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s1 = new Subject<string>();
    var s2 = new Subject<string>();

    var merge = s1.Merge(s2);
    merge.Subscribe(Console.WriteLine);

    s1.OnNext("value1"); //first subject
    s2.OnNext("value2"); //second subject

    Console.ReadLine();
  }
}
