using System.Reactive.Linq;

namespace ObservableGenerate;

internal class Program
{
  static void Main(string[] args)
  {
    //a reactive For statement
    //similar to for(int i=0;i<10;i++)
    var generated = Observable.Generate<int, DateTime>(0, i => i < 10, i => i + 1, i => new DateTime(2016, 1, 1).AddDays(i));

    generated.Subscribe(value => Console.WriteLine("generated -> {0}", value));

    Console.ReadLine();
  }
}
