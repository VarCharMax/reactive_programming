using System.Reactive.Linq;

namespace FromEvent;

internal class Program
{
  static void Main(string[] args)
  {
    //the action from the FromEvent
    Action<string>? fromEventAction = null;

    //setup the FromEvent sequence
    Observable.FromEvent<string>(
        //register the inner action
        innerAction => fromEventAction = innerAction,
        //unregister the inner action
        innerAction => fromEventAction = null)
        .Subscribe(x => Console.WriteLine("-> {0}", x));

    int i = 0;
    while (true)
    {
      //invoke the inner action
      fromEventAction?.Invoke(DateTime.Now.ToString());
      Thread.Sleep(1000);

      if (++i == 5)
      {
        fromEventAction = null;
        break;
      }
    }
  }
}
