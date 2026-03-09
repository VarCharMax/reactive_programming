using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ScanSubject;

internal class Program
{
  static void Main(string[] args)
  {
    var invoiceSummarySubject = new Subject<double>();
    var invoiceSummaryScanSubject = invoiceSummarySubject.Scan((last, x) => x + last);

    //register an observer for printing total amount
    invoiceSummaryScanSubject.Subscribe(new Action<double>(x => Console.WriteLine("Total amount: {0:C}", x)));

    //register some invoice item total
    invoiceSummarySubject.OnNext(1250.50); //add a notebook
    invoiceSummarySubject.OnNext(-50.0); //discount
    invoiceSummarySubject.OnNext(44.98); //a notebook bag

    Console.ReadLine();
  }
}
