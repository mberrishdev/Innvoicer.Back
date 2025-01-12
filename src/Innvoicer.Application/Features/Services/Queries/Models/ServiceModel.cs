using Innvoicer.Domain.Entities.Services;

namespace Innvoicer.Application.Features.Services.Queries.Models;

public class ServiceModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }

    public ServiceModel(Service service)
    {
        Id = service.Id;
        Name = service.Name;
        Price = service.Price;
        Currency = service.Currency;
    }
}
