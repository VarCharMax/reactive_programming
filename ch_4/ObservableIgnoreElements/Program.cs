using System.Reactive.Linq;

using ConsoleObserver;

namespace ObservableIgnoreElements;

internal class Program
{
  static void Main(string[] args)
  {
    //the sourcing sequence of errors or completed messages
    var sourcingSequence = Observable.Range(1, 5).Select(i => { Thread.Sleep(500); return i; });
    sourcingSequence.Subscribe(x => Console.WriteLine("{0}", x));

    //Order of subscription is important.
    var ignoredElements = sourcingSequence.IgnoreElements();
    ignoredElements.Subscribe(new ConsoleObserver<int>());

    Console.ReadLine();
  }
}
