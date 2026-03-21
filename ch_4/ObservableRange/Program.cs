using System.Reactive.Linq;

namespace ObservableRange;

internal class Program
{
  static void Main(string[] args)
  {
    //a ranged sequence
    var range = Observable.Range(0, 1000);

    //an observer will get values
    //anytime it will subscribe
    range.Subscribe(value => Console.WriteLine("range -> {0}", value));

    Console.ReadLine();
  }
}
