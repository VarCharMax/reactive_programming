using System.Reactive.Linq;

namespace ObservableRepeat;

internal class Program
{
  static void Main(string[] args)
  {
    /*
    //Since this is a simple integer sequence, both subscriptions will produce the same sequence of values.
    var sourcingSequence = Observable.Range(1, 5);
    var repeatFor2Times = sourcingSequence.Repeat(2);
    repeatFor2Times.Subscribe(value => Console.WriteLine("Value: {0}", value));
    */

    //Demonstrates that each subscription is evaluated independently,
    //and the sourcing sequence is re-evaluated for each subscription, producing different sequences.
    var sourcingSequence = Observable.Range(1, 5)
        .Select(i => { Thread.Sleep(1000); return i; })
        .Select(i => DateTime.Now);

    var repeatFor2Times = sourcingSequence.Repeat(2);
    repeatFor2Times.Subscribe(value => Console.WriteLine("Value: {0}", value));

    Console.ReadLine();
  }
}
