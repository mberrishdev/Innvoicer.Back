using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Entities.Invoices.Commands;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices;

public class InvoiceItem : Entity<long>
{
    public long InvoiceId { get; private set; }
    [Required, MaxLength(150)] public string Name { get; private set; }
    [Required] public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    [Required, MaxLength(3)] public string Currency { get; private set; }
    [Required] public decimal TotalPrice { get; private set; }
    [Required] public DateTime CheckInDate { get; private set; }

    [Required] public DateTime CheckOutDate { get; private set; }

    [Required] public int Nights { get; private set; }

    private InvoiceItem()
    {
    }

    public InvoiceItem(CreateInvoiceItemCommand command)
    {
        Name = command.Name;
        UnitPrice = command.UnitPrice;
        Quantity = command.Quantity;
        Currency = command.Currency;
        CheckInDate = command.CheckInDate;
        CheckOutDate = command.CheckOutDate;

        Nights = (int)(CheckOutDate - CheckInDate).TotalDays;
        TotalPrice = UnitPrice * Quantity * Nights;
    }
}