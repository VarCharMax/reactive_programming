using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ObservableCreate;

internal class Program
{
  static void Main(string[] args)
  {
    var s1 = Observable.Create<DateTime>(observer =>
    {
      Console.WriteLine("Registering subscriber {0} of type {1}", observer.GetHashCode(), observer);

      //here you can handle by hand your observer interaction logic
      Task.Factory.StartNew(() =>
      {
        //some (time based) message
        for (int i = 0; i < 30; i++)
        {
          observer.OnNext(DateTime.Now);
          Thread.Sleep(1000);
        }
        //end of observer life
        observer.OnCompleted();
      });

      //Takes Action, returns IDisposable. When the returned IDisposable is disposed, the Action will be invoked.
      //This is typically used to clean up resources or unsubscribe from events when the subscription is disposed.
      return Disposable.Create(() => Console.WriteLine("Disposing..."));
      // return () => Console.WriteLine("OnCompleted {0} of type {1}", observer.GetHashCode(), observer);
    });

    var disposableObserver = s1.Subscribe(d => Console.WriteLine("OnNext: {0}", d));

    //wait some time and then press RETURN
    //to dispose the observer
    Console.ReadLine();

    disposableObserver.Dispose();

    Console.ReadLine();
  }
}
