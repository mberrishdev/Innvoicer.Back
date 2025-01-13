using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class CreateInvoiceItemCommand : CommandBase<long>
{
    public string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public string Currency { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }
}