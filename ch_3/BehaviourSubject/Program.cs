using System.Reactive.Subjects;

namespace BehaviourSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var behaviorSubject = new BehaviorSubject<DateTime>(new DateTime(2001, 1, 1));
    Thread.Sleep(1000);

    //the default value will flow to the new subscriber
    behaviorSubject.Subscribe(x => Console.WriteLine(x));
    Thread.Sleep(1000);

    //a new value will flow to the subscriber
    behaviorSubject.OnNext(DateTime.Now);
    Thread.Sleep(1000);

    //this new subscriber will receive the last available message
    //regardless is was not subscribing at the time the message arise
    behaviorSubject.Subscribe(x => Console.WriteLine(x));
    Thread.Sleep(1000);

    Console.ReadLine();
  }
}
