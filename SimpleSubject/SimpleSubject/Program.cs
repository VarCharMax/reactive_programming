using System.Reactive.Subjects;

namespace SimpleSubject
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var simpleSubject = new Subject<string>();
      simpleSubject.OnNext("value1");
      simpleSubject.OnNext("value2");
      simpleSubject.Subscribe(Console.WriteLine);
      simpleSubject.OnNext("value3");
      simpleSubject.OnNext("value4");

      Console.ReadLine();
    }
  }
}
