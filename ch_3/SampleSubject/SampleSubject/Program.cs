using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SampleSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var samplingValueSequence = new Subject<int>();
    var samplingTimeSequence = new Subject<object?>(); // Allow nullable object
    var samplingSequence = samplingValueSequence.Sample(samplingTimeSequence);

    //register an observer
    samplingSequence.Subscribe(new Action<int>(x => Console.WriteLine(x)));

    //some value
    samplingValueSequence.OnNext(10); //ignored
    samplingValueSequence.OnNext(20);

    //raise a message into the sampling time sequence
    samplingTimeSequence.OnNext(null); //last value will be outputted now

    samplingValueSequence.OnNext(30); //ignored
    samplingValueSequence.OnNext(40);

    samplingTimeSequence.OnNext(null); //last value will be outputted now

    samplingValueSequence.OnNext(50); //ignored
    samplingValueSequence.OnNext(60);

    //raise a message into the sampling time sequence
    samplingTimeSequence.OnCompleted(); //last value will be outputted now

    Console.ReadLine();
  }
}
