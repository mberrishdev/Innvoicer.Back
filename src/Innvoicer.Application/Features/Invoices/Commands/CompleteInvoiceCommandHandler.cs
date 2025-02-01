using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Helpers;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Invoices.Commands;

public class CompleteInvoiceCommandHandler(IRepository<Invoice> repository, IMediator mediator)
    : IRequestHandler<CompleteInvoiceCommand>
{
    public async Task Handle(CompleteInvoiceCommand command, CancellationToken cancellationToken)
    {
        var invoice = await repository.GetForUpdateAsync(x => x.Id == command.Id, cancellationToken: cancellationToken)
                      ?? throw new ObjectNotFoundException(nameof(Invoice), nameof(Invoice.Id), command.Id);

        if (invoice.Status != InvoiceStatus.Pending)
            throw new InvalidOperationException("Invoice is not in pending status");

        invoice.Complete(command);

        await repository.UpdateAsync(invoice, cancellationToken);
    }
}