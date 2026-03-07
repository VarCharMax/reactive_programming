using System.Reactive.Subjects;

namespace AsyncSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var asyncSubject = new AsyncSubject<string>();
    asyncSubject.OnNext("value1"); //this will be missed
    asyncSubject.Subscribe(Console.WriteLine);
    asyncSubject.OnNext("value2"); //this will be missed
    asyncSubject.OnNext("value3"); //this will be routed once OnCompleted raised
    Console.ReadLine();
    asyncSubject.OnCompleted();

    Console.ReadLine();
  }
}
