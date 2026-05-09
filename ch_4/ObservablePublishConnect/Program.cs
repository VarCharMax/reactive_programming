using System.Reactive.Linq;

namespace ObservablePublishConnect;

internal class Program
{
  static void Main(string[] args)
  {
    var r = new Random(DateTime.Now.GetHashCode());

    /*
    //a randomic value sequence
    var sourcingSequence = Observable.Range(1, 5)
        .Select(i => { Thread.Sleep(1000); return i; })
        .Select(i => r.Next());

    //multiple subscriptions causing different
    //values being printed onto the console
    sourcingSequence.Subscribe(value => Console.WriteLine("Observer#1: {0}", value));
    sourcingSequence.Subscribe(value => Console.WriteLine("Observer#2: {0}", value));
    sourcingSequence.Subscribe(value => Console.WriteLine("Observer#3: {0}", value));
    
    Console.ReadLine();
    */

    //the sourcing sequence
    var publishedSequence = Observable.Interval(TimeSpan.FromSeconds(0.5))
        .Select(i => DateTime.Now)
        .Publish();

    //attach subscribers before connecting the publisher
    publishedSequence.Subscribe(value => Console.WriteLine("Observer#1: {0}", value));
    publishedSequence.Subscribe(value => Console.WriteLine("Observer#2: {0}", value));
    publishedSequence.Subscribe(value => Console.WriteLine("Observer#3: {0}", value));

    while (true)
    {
      Console.WriteLine("Press RETURN to connect the published sequence");
      Console.ReadLine();

      using var connected = publishedSequence.Connect();
      Console.WriteLine("Press RETURN to quit the connection");
      Console.ReadLine(); //Disposes the using clause.
      //now we disconnected from the published sequence.
    }
  }
}
