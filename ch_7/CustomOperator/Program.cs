using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace CustomOperator;

internal class Program
{
  static void Main(string[] args)
  {
    //1000 items to source from
    var items = Enumerable.Range(0, 1000)
        .Select(x =>
          //raise an exception on item #400
          x == 400 ? throw new ArgumentException("The item #400 has been sourcing") : x);

    //invoke our custom operator
    var sequence = items.AsObservable();

    //output value and metadata
    sequence.Materialize().Subscribe(x => Console.WriteLine("-> {0}", x));

    Console.ReadLine();
  }
}

public static class RxOperators
{
  public static IObservable<T> AsObservable<T>(this IEnumerable<T> source)
  {
    return Observable.Create<T>(observer =>
    {
      foreach (var item in source)
        try
        {
          observer.OnNext(item);
        }
        catch (Exception ex)
        {
          //The raised exception does not flow to the observer.OnError() method, it is simply raised and the execution stops.
          Console.WriteLine("Exception raised: {0}", ex.Message);
          observer.OnError(ex);
          break;
        }

      observer.OnCompleted();
      return Disposable.Empty;
    });
  }
}
