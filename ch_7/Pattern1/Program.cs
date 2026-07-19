using System.Reactive.Linq;

namespace Pattern1;

internal class Program
{
  static void Main(string[] args)
  {
    var values2 = Observable.Range(0, 100).Where(x => x % 2 == 0);
    var values3 = Observable.Range(0, 100).Where(x => x % 3 == 0);
    var values5 = Observable.Range(0, 100).Where(x => x % 5 == 0);

    //flatten sourcing sequences into a new sequence
    //based on the sourcing message index
    var zip = values2.Zip(values3, values5, (a, b, c) => new { a, b, c });

    Console.WriteLine("Zip:");
    zip.Subscribe(x => Console.WriteLine(x));

    Console.ReadLine();

    //create a pattern by grouping messages based on their index
    var pattern = values2.And(values3).And(values5)
        //then produce a single output
        .Then((a, b, c) => new { a, b, c });

    //creates a sequence from the pattern
    var then = Observable.When(pattern);

    Console.WriteLine("Then:");
    then.Subscribe(x => Console.WriteLine(x));

    Console.ReadLine();
  }
}
