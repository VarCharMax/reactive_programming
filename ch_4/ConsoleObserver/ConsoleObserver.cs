namespace ConsoleObserver
{
  public class ConsoleObserver<T> : IObserver<T>
  {
    public void OnCompleted()
    {
      Console.WriteLine("Observer completed!");
    }

    public void OnError(Exception error)
    {
      Console.WriteLine("Observer error: {0}", error);
    }

    public void OnNext(T value)
    {
      Console.WriteLine("Observer value: {0}", value);
    }
  }
}
