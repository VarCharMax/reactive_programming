using System.ComponentModel.DataAnnotations;

using ValidationLib;

namespace InvoiceLib;

public interface ICommand { }

public class CreateNewInvoice : ICommand, IValidable
{
  [Required, Range(1, 100000)]
  public int InvoiceNumber { get; set; }

  [Required]
  public DateTime Date { get; set; }

  [Required(AllowEmptyStrings = false), StringLength(50)]
  public required string CustomerName { get; set; }

  [Required(AllowEmptyStrings = false), StringLength(50)]
  public required string CustomerAddress { get; set; }

  //apply updates
  public static CreateNewInvoice operator +(CreateNewInvoice invoice, UpdateInvoiceCustomerAddress updater)
  {
    return !invoice.InvoiceNumber.Equals(updater.InvoiceNumber)
      ? throw new ArgumentException("Invoice numbers do not match.", nameof(updater))
      : new CreateNewInvoice
      {
        InvoiceNumber = invoice.InvoiceNumber,
        Date = invoice.Date,
        CustomerName = invoice.CustomerName,
        CustomerAddress = updater.CustomerAddress,
      };
  }
}

public class UpdateInvoiceCustomerAddress : ICommand, IValidable
{
  [Required]
  public int InvoiceNumber { get; set; }

  [Required(AllowEmptyStrings = false), StringLength(50)]
  public required string CustomerAddress { get; set; }
}

public class AddInvoiceItem : ICommand, IValidable
{
  [Required]
  public int InvoiceNumber { get; set; }

  [Required]
  public required string ItemCode { get; set; }

  [Required(AllowEmptyStrings = false), StringLength(50)]
  public required string Description { get; set; }

  [Required, Range(1, 10000)]
  public int Amount { get; set; }

  [Required, Range(-10000, 10000)]
  public decimal Price { get; set; }

  public decimal TotalPrice { get { return Amount * Price; } }
}
