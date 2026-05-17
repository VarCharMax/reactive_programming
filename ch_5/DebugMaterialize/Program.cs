using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DebugMaterialize;

internal class Program
{
  static void Main(string[] args)
  {
    //a simple sequence of DateTime
    var sequence1 = new Subject<DateTime>();

    //a console observer
    sequence1.Subscribe(x => Console.WriteLine("{0}", x));

    //a tracing sequence
    //of materialized notifications
    IObservable<Notification<DateTime>> tracingSequence = sequence1.Materialize();
    tracingSequence.Subscribe(notification =>
    {
      //this represents the operation
      Console.WriteLine("Operation: {0}", notification.Kind);

      if (notification.HasValue)
      {
        Console.WriteLine("Value: {0}", notification.Value);
      }
      else if (notification.Exception != null)
      {
        Console.WriteLine("Exception: {0}", notification.Exception);
      }
    });

    //a dematerialized sequence
    var valueSequence = tracingSequence.Dematerialize();
    //a console observer
    valueSequence.Subscribe(x => Console.WriteLine("Demat: {0}", x));

    //flows a new value
    sequence1.OnNext(DateTime.Now);

    //flows the oncomplete message
    sequence1.OnCompleted();

    Console.ReadLine();
  }
}
