using System.Reactive.Linq;

namespace ObservableTimer;

internal class Program
{
  static void Main(string[] args)
  {
    //a timer used for defer the message sending 
    var defer = Observable.Timer(TimeSpan.FromSeconds(5));
    defer.Subscribe(value => Console.WriteLine("defer -> {0}", value));

    //a polling timer that will produce 
    //messages at fixed time interval 
    var loop = Observable.Timer(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(0.5));
    loop.Subscribe(value => Console.WriteLine("loop -> {0}", value));

    Console.ReadLine();
  }
}
