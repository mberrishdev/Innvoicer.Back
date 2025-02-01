using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class PublishInvoiceCommand : CommandBase
{
    public long Id { get; set; }
    public required string Url { get; set; }
}