using System.Text.Json.Serialization;
using Innvoicer.Domain.Entities.Clients.Commands;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices.Commands;

public class UpdateInvoiceCommand : CommandBase
{
    [JsonIgnore] public long Id { get; set; }
    public long CompanyId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? DueDate { get; set; }
    public decimal? DepositAmount { get; set; }

    public List<CreateInvoiceItemCommand> Items { get; set; } = new();
    public UpdateClientCommand Client { get; set; }
}