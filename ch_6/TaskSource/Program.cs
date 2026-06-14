using System.Reactive.Threading.Tasks;

namespace TaskSource;

internal class Program
{
  static void Main(string[] args)
  {
    //as simple task
    var task = Task.Factory.StartNew(() =>
    {
      Thread.Sleep(1000);
      return "ack";
    });

    //a sequence to ack the task's result
    //need using System.Reactive.Threading.Tasks
    var ackSequence = task.ToObservable();

    //some output
    ackSequence.Subscribe(x => Console.WriteLine(x));

    Console.ReadLine();
  }
}
