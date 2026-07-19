using InvoiceLib;

namespace InvoiceProcessor
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //the root sequence of all user input messages
      var commandSource = InvoiceProcessLib.InvoiceProcessor.CreateValidator();

      Console.WriteLine("Return to start saving an invoice");
      Console.ReadLine();

      var invoicenr = new Random(DateTime.Now.GetHashCode()).Next(0, 1000);
      //create a new invoice

      commandSource.OnNext(new CreateNewInvoice { InvoiceNumber = invoicenr, Date = DateTime.Now, CustomerName = string.Empty, CustomerAddress = string.Empty });
      //now a validation error will flow out the sequence
      Console.WriteLine("Return to continue");
      Console.ReadLine();

      //create a valid invoice
      commandSource.OnNext(new CreateNewInvoice { InvoiceNumber = invoicenr, Date = DateTime.Now.Date, CustomerName = "Mr. Red", CustomerAddress = "1234, London Road, Milan, Italy" });
      Console.WriteLine("Return to continue");
      Console.ReadLine();

      //updates the invoice customer address
      commandSource.OnNext(new UpdateInvoiceCustomerAddress { InvoiceNumber = invoicenr, CustomerAddress = "1234, Milan Road, London, UK" });
      Console.WriteLine("Return to continue");
      Console.ReadLine();

      //adds some item
      commandSource.OnNext(new AddInvoiceItem { InvoiceNumber = invoicenr, ItemCode = "WMOUSE", Price = 44.40m, Amount = 10, Description = "Wireless Mouse" });
      Console.WriteLine("Return to continue");
      Console.ReadLine();

      commandSource.OnNext(new AddInvoiceItem { InvoiceNumber = invoicenr, ItemCode = "DMOUSE", Price = 17.32m, Amount = 5, Description = "Wired Mouse" });
      Console.WriteLine("Return to continue");
      Console.ReadLine();

      commandSource.OnNext(new AddInvoiceItem { InvoiceNumber = invoicenr, ItemCode = "USBC1MT", Price = 2.00m, Amount = 100, Description = "Usb cable 1mt" });

      Console.WriteLine("END");
      Console.ReadLine();

    }
  }
}
