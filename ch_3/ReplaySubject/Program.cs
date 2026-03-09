using System.Reactive.Subjects;

namespace ReplaySubject;

internal class Program
{
  static void Main(string[] args)
  {
    var replaySubject = new ReplaySubject<string>();
    var receiverSubject = new Subject<string>();

    receiverSubject.Subscribe(Console.WriteLine);

    replaySubject.OnNext("value1");
    replaySubject.OnNext("value2");
    replaySubject.Subscribe(receiverSubject);
    replaySubject.OnNext("value3");
    replaySubject.OnNext("value4");

    Console.ReadLine();
  }
}
