using System.Reactive.Linq;

namespace ObservableReplay;

internal class Program
{
  static void Main(string[] args)
  {
    //the sourcing sequence will fire
    //for 5 seconds
    var publishedSequence = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(i => DateTime.Now)
        .Take(5)
        .Replay(10);

    //we wait for 10 seconds
    Thread.Sleep(10000);

    //now we connect the subscriber that will
    //recover all messages thanks to the replay behaviour.
    //Even if the subscriber is attached after the sourcing sequence has completed, it will receive current time values.
    publishedSequence.Subscribe(value => Console.WriteLine("Value: {0}", value));
    publishedSequence.Connect();

    Console.ReadLine();
  }
}
