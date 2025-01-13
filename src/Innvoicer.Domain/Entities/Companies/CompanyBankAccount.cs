using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Companies;

public class CompanyBankAccount : Entity<long>
{
    [Required] public long CompanyId { get; private set; }
    [Required, MaxLength(100)] public string BankName { get; private set; }
    [Required, MaxLength(100)] public string Swift { get; private set; }
    [Required, MaxLength(150)] public string Iban { get; private set; }

    public Company Company { get; private set; }

    private CompanyBankAccount()
    {
    }
}