using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WhereSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var s12 = new Subject<string>();
    var filtered = s12.Where(x => x.Contains('e'));
    filtered.Subscribe(Console.WriteLine);

    s12.OnNext("Mr. Brown");
    s12.OnNext("Mr. White");

    Console.ReadLine();
  }
}
