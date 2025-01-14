using System.Text.Json.Serialization;
using Innvoicer.Domain.Entities.Clients.Commands;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class CreateInvoiceCommand : CommandBase<long>
{
    [JsonIgnore] public string? Number { get; set; }
    [JsonIgnore] public string? Key { get; set; }

    public long CompanyId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? DueDate { get; set; }
    public decimal? DepositAmount { get; set; }

    public List<CreateInvoiceItemCommand> Items { get; set; } = new();
    public CreateClientCommand Client { get; set; }
}