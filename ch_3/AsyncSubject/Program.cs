using System.Reactive.Subjects;

namespace AsyncSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var asyncSubject = new AsyncSubject<string>();
    var receiverSubject = new Subject<string>();

    asyncSubject.OnNext("value1"); //this will be missed

    receiverSubject.Subscribe(Console.WriteLine); //Console is observer.
    asyncSubject.Subscribe(receiverSubject); //receiver is observer.

    asyncSubject.OnNext("value2"); //this will be missed
    asyncSubject.OnNext("value3"); //this will be routed once OnCompleted raised

    Console.ReadLine();
    asyncSubject.OnCompleted();

    Console.ReadLine();
  }
}
