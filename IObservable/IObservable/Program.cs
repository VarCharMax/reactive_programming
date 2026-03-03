namespace IObservable
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //this is the message observable responsible for producing messages.
      using (var observer = new ConsoleIntegerProducer())
      //those are the message observer that consume messages
      using (var consumer1 = observer.Subscribe(new IntegerConsumer(2)))
      using (var consumer2 = observer.Subscribe(new IntegerConsumer(3)))
      {
        using (var consumer3 = observer.Subscribe(new IntegerConsumer(5)))
        {
        }
        observer.Wait();
      }

      Console.WriteLine("END");
      Console.ReadLine();
    }
  }

  //Observable able to parse strings from the Console
  //and route numeric messages to all subscribers
  public class ConsoleIntegerProducer : IObservable<int>, IDisposable
  {
    //the subscriber list
    private readonly List<IObserver<int>> subscriberList = [];

    //the cancellation token source for starting/stopping.
    //inner observable working thread.
    private readonly CancellationTokenSource cancellationSource;
    //the cancellation flag.
    private readonly CancellationToken cancellationToken;
    //the running task that runs the inner running thread.
    private readonly Task workerTask;
    public ConsoleIntegerProducer()
    {
      cancellationSource = new CancellationTokenSource();
      cancellationToken = cancellationSource.Token;

      //start the inner worker task that runs the infinite loop for reading console input and routing messages to observers.
      workerTask = Task.Factory.StartNew(OnInnerWorker, cancellationToken);
    }

    //add another observer to the subscriber list
    public IDisposable Subscribe(IObserver<int> observer)
    {
      if (subscriberList.Contains(observer))
        throw new ArgumentException("The observer is already subscribed to this observable");

      Console.WriteLine("Subscribing for {0}", observer.GetHashCode());
      subscriberList.Add(observer);

      return null; // new Unsubscriber(subscriberList, observer);
    }

    //this code executes the observable infinite loop
    //and routes messages to all observers on the valid
    //message handler
    private void OnInnerWorker()
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        var input = Console.ReadLine();

        foreach (var observer in subscriberList)
        {
          if (string.IsNullOrEmpty(input))
            break;
          else if (input.Equals("EXIT"))
          {
            cancellationSource.Cancel();
            break;
          }
          else if (!int.TryParse(input, out int value))
            observer.OnError(new FormatException("Unable to parse given value"));
          else
            observer.OnNext(value);
        }
      }
      cancellationToken.ThrowIfCancellationRequested();
    }

    //cancel main task and ack all observers
    //by sending the OnCompleted message
    public void Dispose()
    {
      if (!cancellationSource.IsCancellationRequested)
      {
        cancellationSource.Cancel();
        while (!workerTask.IsCanceled)
        {
          Thread.Sleep(100);
        }
      }

      cancellationSource.Dispose();
      workerTask.Dispose();

      foreach (var observer in subscriberList)
      {
        observer.OnCompleted();
      }

      // GC.SuppressFinalize(this);
    }

    //wait until the main task completes or went cancelled
    public void Wait()
    {
      while (!(workerTask.IsCompleted || workerTask.IsCanceled))
        Thread.Sleep(100);
    }
  }

  /// <summary>
  /// Consumes numeric values that divides without rest by a given number
  /// </summary>
  public class IntegerConsumer(int validDivider) : IObserver<int>
  {
    readonly int validDivider = validDivider;
    private bool finished = false;
    public void OnCompleted()
    {
      if (finished)
        OnError(new Exception("This consumer already finished it's lifecycle"));
      else
      {
        finished = true;
        Console.WriteLine("{0}: END", GetHashCode());
      }
    }

    public void OnError(Exception error)
    {
      Console.WriteLine("{0}: {1}", GetHashCode(), error.Message);
    }

    public void OnNext(int value)
    {
      if (finished)
        OnError(new Exception("This consumer finished it's lifecycle"));

      //the simple business logic is made by checking divider result
      else if (value % validDivider == 0)
        Console.WriteLine("{0}: {1} divisible by {2}", GetHashCode(), value, validDivider);
    }
  }
}
