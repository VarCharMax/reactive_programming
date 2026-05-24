using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DebugAll;

internal class Program
{
  static void Main(string[] args)
  {
    var r = new Random(DateTime.Now.GetHashCode());

    var stopperSequence = new Subject<bool>();

    //an infinite message source of integer numbers
    //running at 10hz
    var source = Observable.Interval(TimeSpan.FromMilliseconds(100))
        .Select(x => r.Next(1, 20))
        //take only until we press RETURN
        .TakeUntil(stopperSequence);

    source.Subscribe(x => Console.WriteLine(x));

    //we want message metadata
    var all = source.All(x => x < 18).Materialize();

    //All() will fire spontaneously if invalid value encountered in stream, but not synchronously, so the
    //invalid value might appear before or after the All() sequence outputs.
    all.Subscribe(x => Console.WriteLine("FOUND ALL: {0}", x));

    //wait until user press RETURN
    Console.ReadLine();

    //notify the stop message
    stopperSequence.OnNext(true);

    //wait again to see the result.
    Console.ReadLine();
  }
}
