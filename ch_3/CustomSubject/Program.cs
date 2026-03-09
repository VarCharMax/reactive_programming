using System.Reactive.Subjects;

namespace CustomSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var mapper = new MapperSubject<string, double>(x => double.Parse(x));
    mapper.Subscribe(x => Console.WriteLine("{0:N4}", x));

    try
    {
      mapper.OnNext("4.123");
      mapper.OnNext("5.456");
      mapper.OnNext("7.90'?");
      mapper.OnNext("9.432");
    }
    catch (Exception ex)
    {
      Console.WriteLine("An error has occured: {0}", ex.Message);
    }

    Console.ReadLine();
  }
}

public sealed class MapperSubject<Tin, Tout>(Func<Tin, Tout> mapper) : ISubject<Tin, Tout>
{
  public void OnCompleted()
  {
    foreach (var o in _observers.ToArray())
    {
      o.OnCompleted();
      _observers.Remove(o);
    }
  }

  public void OnError(Exception error)
  {
    foreach (var o in _observers.ToArray())
    {
      o.OnError(error);
      _observers.Remove(o);
    }
  }

  public void OnNext(Tin value)
  {
    Tout newValue;

    try
    {
      //mapping statement
      newValue = mapper(value);
    }
    catch (Exception ex)
    {
      //if mapping crashed
      OnError(ex);
      return;
    }

    //if mapping succeded
    foreach (var o in _observers)
    {
      o.OnNext(newValue);
    }
  }

  //all registered observers
  private readonly List<IObserver<Tout>> _observers = [];

  public IDisposable Subscribe(IObserver<Tout> observer)
  {
    _observers.Add(observer);
    return new ObserverHandler<Tout>(observer, OnObserverLifecycleEnd);
  }

  private void OnObserverLifecycleEnd(IObserver<Tout> o)
  {
    o.OnCompleted();
    _observers.Remove(o);
  }

  //this class simply informs the subject that a dispose
  //has been invoked against the observer causing its removal
  //from the observer collection of the subject
  private class ObserverHandler<T>(IObserver<T> observer, Action<IObserver<T>> onObserverLifecycleEnd) : IDisposable
  {
    public void Dispose()
    {
      onObserverLifecycleEnd(observer);
    }
  }
}