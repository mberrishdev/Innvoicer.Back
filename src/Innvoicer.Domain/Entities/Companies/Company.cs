using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Entities.Users;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Companies;

public class Company : Entity<long>
{
    [Required, MaxLength(100)] public string Name { get; private set; }
    public string? Logo { get; private set; }
    [Required, MaxLength(50)] public string VatOrPersonalCode { get; private set; }

    [Required, MaxLength(100)] public string StreetAddress { get; private set; }
    [Required, MaxLength(100)] public string City { get; private set; }
    [Required, MaxLength(100)] public string PostalCode { get; private set; }
    [Required, MaxLength(100)] public string Country { get; private set; }

    public string? AdditionalInformation { get; private set; }

    [Required] public long UserId { get; private set; }
    public User User { get; private set; }

    public ICollection<CompanyBankAccount>
        CompanyBankAccounts { get; private set; }

    private Company()
    {
    }
}