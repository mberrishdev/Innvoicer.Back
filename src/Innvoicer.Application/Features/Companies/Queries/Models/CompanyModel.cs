using System.Collections.Generic;
using System.Linq;
using Innvoicer.Domain.Companies;

namespace Innvoicer.Application.Features.Companies.Queries.Models;

public class CompanyModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string VatOrPersonalCode { get; set; }

    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public string? AdditionalInformation { get; set; }

    public List<CompanyBankAccountModel> BankAccounts { get; set; }

    public CompanyModel(Company company)
    {
        Id = company.Id;
        Name = company.Name;
        Logo = company.Logo;
        VatOrPersonalCode = company.VatOrPersonalCode;
        StreetAddress = company.StreetAddress;
        City = company.City;
        PostalCode = company.PostalCode;
        Country = company.Country;
        AdditionalInformation = company.AdditionalInformation;

        // Mapping bank accounts
        BankAccounts = company.CompanyBankAccounts.Select(cba => new CompanyBankAccountModel(cba)).ToList();
    }
}

public class CompanyBankAccountModel
{
    public string BankName { get; set; }
    public string Swift { get; set; }
    public string Iban { get; set; }

    public CompanyBankAccountModel(CompanyBankAccount companyBankAccount)
    {
        BankName = companyBankAccount.BankName;
        Swift = companyBankAccount.Swift;
        Iban = companyBankAccount.Iban;
    }
}