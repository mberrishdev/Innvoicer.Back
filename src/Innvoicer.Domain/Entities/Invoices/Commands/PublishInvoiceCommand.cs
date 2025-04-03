using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class PublishInvoiceCommand : CommandBase
{
    public long Id { get; set; }
    public string? Url { get; set; }
}