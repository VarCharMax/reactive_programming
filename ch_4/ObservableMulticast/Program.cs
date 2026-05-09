using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ObservableMulticast;

internal class Program
{
  static void Main(string[] args)
  {
    //the sourcing sequence
    var sourcingSequence = Observable.Interval(TimeSpan.FromSeconds(1))
        .Select(i => DateTime.Now);

    //the subject that will route messages
    var multicastingSubject = new Subject<DateTime>();

    //the publisher sequence
    var multicastSequence = sourcingSequence.Multicast(multicastingSubject);

    //subscribers
    multicastSequence.Subscribe(value => Console.WriteLine("Observer#1: {0}", value));
    multicastSequence.Subscribe(value => Console.WriteLine("Observer#2: {0}", value));

    //connect the publisher sequence
    multicastSequence.Connect();

    Console.ReadLine();
  }
}
