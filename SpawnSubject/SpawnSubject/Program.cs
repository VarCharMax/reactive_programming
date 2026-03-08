using System.Reactive.Subjects;

namespace SpawnSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var senderSubject = new Subject<string>();

    var receiverSubject = new Subject<string>();
    receiverSubject.Subscribe(x => Console.WriteLine("s1=>{0}", x));

    senderSubject.Subscribe(receiverSubject);

    var routerSubject = Subject.Create<string>(receiverSubject, senderSubject);
    routerSubject.Subscribe(x => Console.WriteLine("s3=>{0}", x));

    senderSubject.OnNext("value1");
    senderSubject.OnNext("value2");

    Console.ReadLine();
  }
}