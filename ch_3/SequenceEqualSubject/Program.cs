using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SequenceEqualSubject
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var s18 = new Subject<int>();
      var s19 = new Subject<int>();

      var equals = s18.SequenceEqual(s19);
      equals.Subscribe(x => Console.WriteLine("sequenceEqual: {0}", x));

      s18.OnNext(10);
      s18.OnNext(20);

      s19.OnNext(10);
      s19.OnNext(20);

      s18.OnNext(30);

      s19.OnNext(30);
      // s19.OnNext(31);

      //completes to flow out the sequenceEqual result message
      s18.OnCompleted();
      s19.OnCompleted();

      Console.ReadLine();
    }
  }
}
