using Common.Repository.Repository;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;

namespace Innvoicer.Application.Features.Invoices.Commands;

public class CreateInvoiceCommandHandler
    (IRepository<Invoice> repository, IMediator mediator) : IRequestHandler<CreateInvoiceCommand, long>
{
    public async Task<long> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        //check companyId
        var lastInvoice =
            await repository.GetAsync(x => x.CompanyId == command.CompanyId, cancellationToken: cancellationToken);
        command.Number = lastInvoice?.Number?.ToString() ?? 1.ToString();

        var invoice = new Invoice(command);

        await repository.InsertAsync(invoice, cancellationToken);

        return invoice.Id;
    }
}