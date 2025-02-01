using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class CompleteInvoiceCommand : CommandBase
{
    public long Id { get; set; }
}