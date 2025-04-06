using Common.Repository.Repository;
using Innvoicer.Application.Features.Companies.Queries;
using Innvoicer.Application.Helpers;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;

namespace Innvoicer.Application.Features.Invoices.Commands;

public class CreateInvoiceCommandHandler(IRepository<Invoice> repository, IMediator mediator)
    : IRequestHandler<CreateInvoiceCommand, long>
{
    public async Task<long> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(new CheckCompanyExistQuery(command.CompanyId), cancellationToken);
        
        var lastInvoice =
            await repository.GetAsync(x => x.CompanyId == command.CompanyId, cancellationToken: cancellationToken);
        command.Number = lastInvoice?.Number?.ToString() ?? 1.ToString();

        command.Key = HashHelper.HashFNV1a($"{command.CompanyId}{command.Number}{DateTime.Now}");
        var invoice = new Invoice(command);

        await repository.InsertAsync(invoice, cancellationToken);

        return invoice.Id;
    }
}