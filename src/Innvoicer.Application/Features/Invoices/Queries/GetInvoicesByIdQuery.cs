using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Features.Invoices.Queries.Models;
using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Entities.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Invoices.Queries;

public sealed record GetInvoicesByIdQuery(long Id) : IRequest<InvoiceModel>;

public class GetInvoicesByIdQueryHandler(IQueryRepository<Invoice> repository)
    : IRequestHandler<GetInvoicesByIdQuery, InvoiceModel>
{
    public async Task<InvoiceModel> Handle(GetInvoicesByIdQuery request, CancellationToken cancellationToken)
    {
        var rp = new List<Func<IQueryable<Invoice>, IIncludableQueryable<Invoice, object>>>
        {
            x => x.Include(invoice => invoice.Items),
            x => x.Include(invoice => invoice.Client),
            x => x.Include(invoice => invoice.Company).ThenInclude(c => c.CompanyBankAccounts)
        };

        //todo check if company is user company
        var invoice = await repository.GetAsync(predicate: x => x.Id == request.Id && x.Status != InvoiceStatus.Deleted,
                          rp,
                          cancellationToken: cancellationToken) ??
                      throw new ObjectNotFoundException(nameof(Invoice), nameof(Invoice.Id), request.Id);

        return new InvoiceModel(invoice);
    }
}