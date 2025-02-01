using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Helpers;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Invoices.Commands;

public class PublishInvoiceCommandHandler(IRepository<Invoice> repository, IMediator mediator)
    : IRequestHandler<PublishInvoiceCommand>
{
    public async Task Handle(PublishInvoiceCommand command, CancellationToken cancellationToken)
    {
        var invoice = await repository.GetForUpdateAsync(x => x.Id == command.Id, cancellationToken: cancellationToken)
                      ?? throw new ObjectNotFoundException(nameof(Invoice), nameof(Invoice.Id), command.Id);
        
        //todo check status

        invoice.Publish(command);

        //send sms
        await repository.UpdateAsync(invoice, cancellationToken);
    }
}