using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Entities.Clients;
using Innvoicer.Domain.Entities.Invoices.Commands;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Invoices;

public class Invoice : Entity<long>
{
    [Required] public string Key { get; private set; }
    [Required] public string Number { get; private set; }

    [Required] public long CompanyId { get; private set; }

    [Required] public long ClientId { get; private set; }

    public DateTime IssueDate { get; private set; }
    [Required] public DateTime? DueDate { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal? DepositAmount { get; private set; }

    public List<InvoiceItem> Items { get; private set; } = new List<InvoiceItem>();


    [Required] public DateTime CreatedAt { get; private set; } = DateTimeHelper.Now;
    public DateTime? UpdatedAt { get; private set; }

    public Client Client { get; private set; }
    public Company Company { get; private set; }

    private Invoice()
    {
    }

    public Invoice(CreateInvoiceCommand command)
    {
        Key = command.Key!;
        Number = command.Number!;
        CompanyId = command.CompanyId;
        IssueDate = command.IssueDate;
        DueDate = command.DueDate;
        Status = InvoiceStatus.Draft;
        DepositAmount = command.DepositAmount;
        CreatedAt = DateTimeHelper.Now;
        Items = command.Items.Select(item => new InvoiceItem(item)).ToList();
        Client = new Client(command.Client);
        TotalAmount = Items.Sum(x => x.TotalPrice);
    }

    public void Update(UpdateInvoiceCommand command)
    {
        CompanyId = command.CompanyId;
        IssueDate = command.IssueDate;
        DueDate = command.DueDate;
        DepositAmount = command.DepositAmount;
        UpdatedAt = DateTimeHelper.Now;
        Items = command.Items.Select(item => new InvoiceItem(item)).ToList();
        Client.Update(command.Client);
        TotalAmount = Items.Sum(x => x.TotalPrice);
    }

    public void Delete(DeleteInvoiceCommand command)
    {
        Status = InvoiceStatus.Deleted;
        UpdatedAt = DateTimeHelper.Now;
    }

    public void Publish(PublishInvoiceCommand command)
    {
        Status = InvoiceStatus.Pending;
        UpdatedAt = DateTimeHelper.Now;
    }
}