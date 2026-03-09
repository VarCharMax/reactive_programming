using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DistinctUntilChangedSubject
{
  internal class Program
  {
    static void Main(string[] args)
    {

      var s12 = new Subject<string>();
      var distinct = s12.DistinctUntilChanged();

      distinct.Subscribe(Console.WriteLine);

      s12.OnNext("value1"); //ok
      s12.OnNext("value2"); //ok
      s12.OnNext("value2"); //ignored
      s12.OnNext("value3"); //ok
      s12.OnNext("value4"); //ok
      s12.OnNext("value1"); //ok
      s12.OnNext("value2"); //ok
      s12.OnNext("value2"); //ignored
      s12.OnNext("value3"); //ok
      s12.OnNext("value4"); //ok

      Console.ReadLine();
    }
  }
}
