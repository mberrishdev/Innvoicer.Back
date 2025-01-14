using Innvoicer.Application.Features.Companies.Queries.Models;
using Innvoicer.Domain.Entities.Clients;
using Innvoicer.Domain.Entities.Invoices;

namespace Innvoicer.Application.Features.Invoices.Queries.Models;

public class InvoiceModel
{
    public long Id { get; set; }
    public string Key { get; }
    public string Number { get; }
    public long CompanyId { get; }
    public long ClientId { get; }
    public DateTime IssueDate { get; }
    public DateTime? DueDate { get; }
    public InvoiceStatus Status { get; }
    public decimal TotalAmount { get; }
    public decimal? DepositAmount { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public ClientModel Client { get; }
    public CompanyModel Company { get; }
    public List<InvoiceItemModel> Items { get; }

    public InvoiceModel(Invoice invoice)
    {
        Id = invoice.Id;
        Key = invoice.Key;
        Number = invoice.Number;
        CompanyId = invoice.CompanyId;
        ClientId = invoice.ClientId;
        IssueDate = invoice.IssueDate;
        DueDate = invoice.DueDate;
        Status = invoice.Status;
        TotalAmount = invoice.TotalAmount;
        DepositAmount = invoice.DepositAmount;
        CreatedAt = invoice.CreatedAt;
        UpdatedAt = invoice.UpdatedAt;
        Client = new ClientModel(invoice.Client);
        Items = invoice.Items.Select(item => new InvoiceItemModel(item)).ToList();
        Company = new CompanyModel(invoice.Company);
    }
}

public class ClientModel
{
    public string Name { get; }
    public string? PersonalId { get; set; }
    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }


    public ClientModel(Client client)
    {
        Name = client.Name;
        PersonalId = client.PersonalId;
        Address = client.Address;
        Email = client.Email;
        Phone = client.Phone;
    }
}

public class InvoiceItemModel
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Currency { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int Nights { get; set; }

    public InvoiceItemModel(InvoiceItem item)
    {
        Name = item.Name;
        UnitPrice = item.UnitPrice;
        Quantity = item.Quantity;
        Currency = item.Currency;
        TotalPrice = item.TotalPrice;
        CheckInDate = item.CheckInDate;
        CheckOutDate = item.CheckOutDate;
        Nights = item.Nights;
    }
}