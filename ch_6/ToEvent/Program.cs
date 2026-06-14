using System.Reactive.Linq;

namespace ToEvent;

internal class Program
{
  static void Main(string[] args)
  {
    //an infinite sequence
    var sequence = Observable.Interval(TimeSpan.FromSeconds(1)).Select(x => DateTime.Now);

    //the event wrapper
    var eventWrapper = sequence.ToEvent();

    //register the event handler
    eventWrapper.OnNext += x => Console.WriteLine("{0}", x);

    Console.ReadLine();
  }
}
