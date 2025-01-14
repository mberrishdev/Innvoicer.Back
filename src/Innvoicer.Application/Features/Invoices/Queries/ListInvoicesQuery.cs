using Common.Repository.Repository;
using Innvoicer.Application.Features.Invoices.Queries.Models;
using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Entities.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Invoices.Queries;

public sealed record ListInvoicesQuery(long CompanyId) : IRequest<List<InvoiceModel>>;

public class ListInvoicesQueryHandler(IQueryRepository<Invoice> repository)
    : IRequestHandler<ListInvoicesQuery, List<InvoiceModel>>
{
    public async Task<List<InvoiceModel>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
    {
        var rp = new List<Func<IQueryable<Invoice>, IIncludableQueryable<Invoice, object>>>
        {
            x => x.Include(invoice => invoice.Items),
            x => x.Include(invoice => invoice.Client),
            x => x.Include(invoice => invoice.Company).ThenInclude(c => c.CompanyBankAccounts)
        };

        //todo check if company is user company
        var invoices = await repository.GetListAsync(rp, predicate: x => x.CompanyId == request.CompanyId,
            cancellationToken: cancellationToken);

        return invoices.Select(invoice => new InvoiceModel(invoice)).ToList();
    }
}