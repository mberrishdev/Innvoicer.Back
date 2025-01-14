using System.Text.Json.Serialization;

namespace Innvoicer.Domain.Entities.Clients.Commands;

public class UpdateClientCommand : CreateClientCommand
{
    [JsonIgnore] public long Id { get; set; }
}