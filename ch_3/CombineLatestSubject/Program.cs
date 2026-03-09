using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CombineLatestSubject
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var s6 = new Subject<string>();
      var s7 = new Subject<int>();

      var clatest = s6.CombineLatest(s7, (x, y) => new { text = x, value = y, });
      clatest.Subscribe(x => Console.WriteLine("{0}: {1}", x.text, x.value));

      //some message
      s6.OnNext("Mr. Brown");
      s7.OnNext(10);
      s7.OnNext(20);
      s6.OnNext("Mr. Green");
      s6.OnNext("Mr. White");
      s7.OnNext(30);

      Console.ReadLine();
    }
  }
}
