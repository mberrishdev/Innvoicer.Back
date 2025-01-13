using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Services;

public class Service : Entity<long>
{
    [Required, MaxLength(150)] public string Name { get; private set; }
    [Required] public decimal Price { get; private set; }

    [Required, MaxLength(3)] public  string Currency { get; private set; }

    [Required] public long CompanyId { get; private set; }

    public Company Company { get; set; }
}