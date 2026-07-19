using System.Reactive.Linq;

namespace Pattern2;

internal class Program
{
  static void Main(string[] args)
  {
    var values2 = Observable.Range(0, 100).Where(x => x % 2 == 0);
    var values3 = Observable.Range(0, 100).Where(x => x % 3 == 0);
    var values5 = Observable.Range(0, 100).Where(x => x % 5 == 0);

    //multiple patterns
    var values7 = Observable.Range(0, 100).Where(x => x % 7 == 0);
    var values9 = Observable.Range(0, 100).Where(x => x % 9 == 0);
    var values11 = Observable.Range(0, 100).Where(x => x % 11 == 0);

    var pattern = values2.And(values3)
      .And(values5)
      .Then((a, b, c) => new { a, b, c }); //20 elements

    var pattern79 = values7
      .And(values9)
      .And(values11)
      .Then((a, b, c) => new { a, b, c, }); //10 elements

    //flatten multiple sourcing pattern into a new sequence
    var then79 = Observable.When(pattern, pattern79);

    //the message order will follow the sourcing patterns message index
    //in a 2/1 and 1/2 ratio, the first pattern will produce 20 messages and the second pattern will produce 10 messages
    Console.WriteLine("Then79:");
    then79.Subscribe(x => Console.WriteLine(x));

    Console.ReadLine();
  }
}
