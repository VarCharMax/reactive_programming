using System.Reactive.Linq;

namespace ObservablePublishLast;

internal class Program
{
  static void Main(string[] args)
  {
    //the sourcing sequence
    var publishedSequence = Observable.Interval(TimeSpan.FromSeconds(0.5))
        .Select(i => DateTime.Now)
        .Take(5)
        .PublishLast();

    publishedSequence.Subscribe(value => Console.WriteLine("Last: {0}", value));
    publishedSequence.Connect();

    Console.ReadLine();
  }
}
