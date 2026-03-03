namespace FilteringEvents
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Watching for new files");
      using var publisher = new NewFileSavedMessagePublisher(@"[WRITE A PATH HERE]");
      using var filter = new StringMessageFilter(".txt");
      //subscribe the filter to publisher messages
      publisher.Subscribe(filter);
      //subscribe the console subscriber to the filter
      //instead that directly to the publisher
      filter.Subscribe(new NewFileSavedMessageSubscriber());

      Console.WriteLine("Press RETURN to exit");
      Console.ReadLine();
    }
  }

  public sealed class NewFileSavedMessagePublisher : IObservable<string>, IDisposable
  {
    private readonly FileSystemWatcher watcher;
    public NewFileSavedMessagePublisher(string path)
    {
      //creates a new file system event router
      this.watcher = new FileSystemWatcher(@"C:\Users\USER\Desktop\4954\ss");
      //register for handling File Created event
      this.watcher.Created += Watcher_Created;
      //enable event routing
      this.watcher.EnableRaisingEvents = true;
    }

    //signal all observers a new file arrived
    void Watcher_Created(object sender, FileSystemEventArgs e)
    {
      foreach (var observer in subscriberList)
        observer.OnNext(e.FullPath);
    }

    //the subscriber list
    private readonly List<IObserver<string>> subscriberList = [];

    public IDisposable Subscribe(IObserver<string> observer)
    {
      //register the new observer
      subscriberList.Add(observer);

      return null;
    }

    public void Dispose()
    {
      //disable file system event routing
      this.watcher.EnableRaisingEvents = false;
      //deregister from watcher event handler
      this.watcher.Created -= Watcher_Created;
      //dispose the watcher
      this.watcher.Dispose();

      //signal all observers that job is done
      foreach (var observer in subscriberList)
        observer.OnCompleted();
    }
  }

  /// <summary>
  /// A tremendously basic implementation
  /// </summary>
  public sealed class NewFileSavedMessageSubscriber : IObserver<string>
  {
    public void OnCompleted()
    {
      Console.WriteLine("-> END");
    }

    public void OnError(Exception error)
    {
      Console.WriteLine("-> {0}", error.Message);
    }

    public void OnNext(string value)
    {
      Console.WriteLine("-> {0}", value);
    }
  }

  /// <summary>
  /// The filtering observable/observer
  /// </summary>
  public sealed class StringMessageFilter(string filter) : IObservable<string>, IObserver<string>, IDisposable
  {

    //the observer collection
    private readonly List<IObserver<string>> observerList = [];
    public IDisposable Subscribe(IObserver<string> observer)
    {
      this.observerList.Add(observer);
      return null;
    }

    //a simple implementation
    //that disables message routing once
    //the OnCompleted has been invoked
    private bool hasCompleted = false;
    public void OnCompleted()
    {
      hasCompleted = true;
      foreach (var observer in observerList)
        observer.OnCompleted();
    }

    //routes error messages until not completed
    public void OnError(Exception error)
    {
      if (!hasCompleted)
        foreach (var observer in observerList)
          observer.OnError(error);
    }

    //routes valid messages until not completed
    public void OnNext(string value)
    {
      Console.WriteLine("Filtering {0}", value);

      if (!hasCompleted && value.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
        foreach (var observer in observerList)
          observer.OnNext(value);
    }

    public void Dispose()
    {
      OnCompleted();
    }
  }
}
