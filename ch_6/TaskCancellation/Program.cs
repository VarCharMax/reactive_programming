using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace TaskCancellation;

internal class Program
{
  static void Main(string[] args)
  {
    var fromDatabase = Observable.Create<DateTime>(o =>
    {
      //a cancellation token source for timeout
      var tks = new CancellationTokenSource(TimeSpan.FromSeconds(5));
      var token = tks.Token;

      //the cancellable task within the sequence
      return Task.Factory.StartNew(() =>
      {
        int i = 0;
        //run until cancel requested
        while (!token.IsCancellationRequested)
        {
          //using (var cn = new SqlConnection(@"data source=(local);integrated security=true;"))
          //using (var cm = new SqlCommand("select getdate()", cn))
          //{
          Thread.Sleep(1000);
          //cn.Open();
          //read time from DB
          o.OnNext(DateTime.Now);
          //}

          if (++i == 5)
          {
            //signal oncompleted
            o.OnCompleted();
          }
        }

        //returns a disposable subscription completed object
        //with an OnCompleted callback
        return Disposable.Create(() => Console.WriteLine("Killing subscription"));
      }, token);
    });

    fromDatabase.Subscribe(x => Console.WriteLine(x));

    Console.ReadLine();
  }
}
