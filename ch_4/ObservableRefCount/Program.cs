using System.Reactive.Linq;

namespace ObservableRefCount;

internal class Program
{
  static void Main(string[] args)
  {
    //the sourcing sequence
    var publishedSequence = Observable.Interval(TimeSpan.FromSeconds(0.5))
        .Select(i => DateTime.Now)
        .Publish()
        .RefCount();

    while (true)
    {
      Console.WriteLine("Press return to subscribe");
      Console.ReadLine();
      using var subscription = publishedSequence.Subscribe(value => Console.WriteLine("Observer: {0}", value));
      Console.WriteLine("Press return to unsubscribe");
      Console.ReadLine();
      //now we disconnected from the published sequence
    }
  }
}
