using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Helpers;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Invoices.Commands;

public class UpdateInvoiceCommandHandler(IRepository<Invoice> repository, IMediator mediator)
    : IRequestHandler<UpdateInvoiceCommand>
{
    public async Task Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken)
    {
        var rp = new List<Func<IQueryable<Invoice>, IIncludableQueryable<Invoice, object>>>
        {
            x => x.Include(invoice => invoice.Items),
            x => x.Include(invoice => invoice.Client),
        };


        var invoice = await repository.GetForUpdateAsync(x => x.Id == command.Id, rp, cancellationToken: cancellationToken)
                      ?? throw new ObjectNotFoundException(nameof(Invoice), nameof(Invoice.Id), command.Id);

        
        //todo check status

        //todo check companyId

        invoice.Update(command);

        await repository.UpdateAsync(invoice, cancellationToken);
    }
}