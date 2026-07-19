using System.Reactive.Linq;
using System.Reactive.Subjects;

using InvoiceLib;

using ValidationLib;

namespace InvoiceProcessLib
{
  public class InvoiceProcessor
  {
    public static Subject<ICommand> CreateValidator()
    {
      //the root sequence of all user input messages
      var commandSource = new Subject<ICommand>();

      //register the diagnostic output of all messages
      commandSource.Materialize().Subscribe(Console.WriteLine);

      //register validation error output
      var validables = commandSource
          //routes only validable messages
          .OfType<IValidable>()
          //convert messages into validation results
          .Select(x => ValidableObjectHelper.Validate(x));

      //filter in search of invalid messages
      validables.Where(x => !x.IsValid)
          //notify the error on the output
          .Subscribe(x => Console.WriteLine("Validation errors: {0}", string.Join(",", x.Result)));

      //filter in search of valid messages
      _ = validables.Where(static x => x.IsValid)
          //get back the command message
          .Select(static x => x.Instance as ICommand)
          // filter out nulls to satisfy non-nullable object constraint
          .Where(static x => x is not null)
          // cast to object using Select to avoid nullability issues
          .Select(static x => (object)x!)
          //routes only invoice item messages
          .OfType<AddInvoiceItem>()
          //group items per invoice
          .GroupBy(x => x.InvoiceNumber)
          .Subscribe(group => group
              //project the message to a new shape for getting the result
              .Select(x => new { NewItem = x, x.TotalPrice })
              //apply the accumulator function to get the result
              .Scan((old, x) => new { x.NewItem, TotalPrice = old.TotalPrice + x.TotalPrice })
              //output the result
              .Subscribe(x => Console.WriteLine("Current total amount: {0:N2}", x.TotalPrice))
          );

      //filter in search of valid messages
      _ = validables.Where(x => x.IsValid)
          //get back the command message
          .Select(x => x.Instance as ICommand)
          // filter out nulls to satisfy non-nullable object constraint
          .Where(static x => x is not null)
          // cast to object using Select to avoid nullability issues
          .Select(static x => (object)x!)
          //routes only new invoices or invoice updates messages
          .Where(x => x is CreateNewInvoice || x is UpdateInvoiceCustomerAddress)
          //group items per invoice
          .GroupBy(static x => x is CreateNewInvoice invoice ? invoice.InvoiceNumber : ((UpdateInvoiceCustomerAddress)x).InvoiceNumber)
          .Subscribe(static group => group
              //apply the updates to get the last state
              //a custom "+" operator to apply updates to the original invoice
              //is available into the CreateNewInvoice class
              .Scan((old, x) => x is CreateNewInvoice invoice ? invoice : (CreateNewInvoice)old + (UpdateInvoiceCustomerAddress)x)
              //change type
              .OfType<CreateNewInvoice>()
              //output the new invoice details
              .Subscribe(x => Console.WriteLine("Available an invoice nr: {0} to {1} living in {2}", x.InvoiceNumber, x.CustomerName, x.CustomerAddress))
          );

      return commandSource;
    }
  }
}
