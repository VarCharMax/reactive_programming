using System.Reactive.Linq;

namespace StaticEvent
{
  internal class Program
  {
    public static event Action? MyStaticEvent;

    static void Main(string[] args)
    {
      //event sequence
      var sequence = Observable.FromEvent(
          //register the inner action as handler of the static event
          x => MyStaticEvent += x,
          //unregister the inner action from the static event
          x => MyStaticEvent -= x);

      //observer
      sequence.Subscribe(unit => Console.WriteLine(unit));

      //manually raise the event
      MyStaticEvent?.Invoke();

      Console.ReadLine();
    }
  }
}
