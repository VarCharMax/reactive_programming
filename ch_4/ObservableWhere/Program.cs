using System.Reactive.Linq;

namespace ObservableWhere;

internal class Program
{
  static void Main(string[] args)
  {
    var fixedTimeBasedSequence = Observable.Interval(TimeSpan.FromSeconds(1));

    //convert the message 
    //into time value
    var dateTimeSequence = fixedTimeBasedSequence
        .Select(v => DateTime.UtcNow);

    //filtered sequence of times with even second value
    var filteredSequence = dateTimeSequence.Where(dt => dt.Second % 2 == 0);

    filteredSequence.Subscribe(value => Console.WriteLine("{0}", value));

    Console.ReadLine();
  }
}
