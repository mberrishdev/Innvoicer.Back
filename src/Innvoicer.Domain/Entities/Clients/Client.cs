using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Entities.Clients.Commands;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Clients;

public class Client : Entity<long>
{
    [Required, MaxLength(150)] public string Name { get; private set; }
    [MaxLength(15)] public string? PersonalId { get; private set; }
    [MaxLength(150)] public string? Address { get; private set; }

    [EmailAddress] public string? Email { get; private set; }

    [MaxLength(15)] public string? Phone { get; private set; }

    public ICollection<Invoice> Invoices { get; private set; } = new List<Invoice>();

    private Client()
    {
    }

    public Client(CreateClientCommand command)
    {
        Name = command.Name;
        PersonalId = command.PersonalId;
        Email = command.Email;
        Phone = command.Phone;
        Address = command.Address;
    }

    public void Update(UpdateClientCommand command)
    {
        Name = command.Name;
        PersonalId = command.PersonalId;
        Email = command.Email;
        Phone = command.Phone;
        Address = command.Address;
    }
}