using System.Reactive.Linq;

namespace FromEventPattern;

internal class Program
{
  public static event EventHandler? UserIsTiredEvent;

  static void Main(string[] args)
  {
    //raise the event in 5 seconds
    Task.Factory.StartNew(() =>
    {
      Thread.Sleep(5000);

      //check event is handled
      UserIsTiredEvent?.Invoke("Program.Main", new EventArgs());
    });

    //classic event handler registration
    UserIsTiredEvent += EventHandler1;

    //reactive registration
    var eventSequence = Observable.FromEventPattern(typeof(Program), "UserIsTiredEvent");

    //some output
    eventSequence.Materialize().Subscribe(x => Console.WriteLine("From Rx: {0}", x));

    Console.ReadLine();
  }

  static void EventHandler1(object? o, EventArgs e)
  {
    Console.WriteLine("Handling for object {0}", o);
  }
}