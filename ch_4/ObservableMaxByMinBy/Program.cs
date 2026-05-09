using System.Reactive.Linq;

namespace ObservableMaxByMinBy;

internal class Program
{
  static void Main(string[] args)
  {
    //a sourcing sequence of 2 messages per second - there can potentially be two messages with the same timestamp, so we will have multiple maxby/minby values.
    var sourcingSequence = Observable.Interval(TimeSpan.FromSeconds(0.5))
        //a transformation into DateTime
        //skipping milliseconds/nanoseconds
        .Select(id => new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second))
        //we take messages only for 10 seconds
        .TakeUntil(DateTimeOffset.Now.AddSeconds(10));

    //the maxby sequence
    var maxBySequence = sourcingSequence.MaxBy(d => d.Ticks);
    maxBySequence.Subscribe(ordered =>
    {
      foreach (var value in ordered)
      {
        Console.WriteLine("MaxBy: {0}", value);
      }
    });

    //the minby sequence
    var minBySequence = sourcingSequence.MinBy(d => d.Ticks); //100 nanoseconds or one ten-millionth of a second.
    minBySequence.Subscribe(ordered =>
    {
      foreach (var value in ordered)
      {
        Console.WriteLine("MinBy: {0}", value);
      }
    });

    Console.ReadLine();
  }

  //a non-primitive type implementing IComparable.
  public class MyType : IComparable
  {
    public string Name { get; set; } = "";

    public int CompareTo(object? obj)
    {
      return this.Name.CompareTo(obj);
    }
  }
}

