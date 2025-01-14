using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class DeleteInvoiceCommand : CommandBase
{
    public long Id { get; set; }
}