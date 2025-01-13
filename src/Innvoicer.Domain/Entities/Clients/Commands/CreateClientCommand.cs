namespace Innvoicer.Domain.Entities.Clients.Commands;

public class CreateClientCommand
{
    public required string Name { get; set; }
    public required string? PersonalId { get; set; }
    public required string? Address { get; set; }
    public required string? Email { get; set; }
    public required string? Phone { get; set; }
}