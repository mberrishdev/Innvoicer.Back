using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Helpers;
using Innvoicer.Application.Infrastructure.Contracts.SmsServices;
using Innvoicer.Domain.Entities.Invoices;
using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Invoices.Commands;

public class PublishInvoiceCommandHandler(IRepository<Invoice> repository, ISmsService smsService)
    : IRequestHandler<PublishInvoiceCommand>
{
    public async Task Handle(PublishInvoiceCommand command, CancellationToken cancellationToken)
    {
        var rp = new List<Func<IQueryable<Invoice>, IIncludableQueryable<Invoice, object>>>
        {
            x => x.Include(invoice => invoice.Company),
            x => x.Include(invoice => invoice.Client)
        };

        var invoice = await repository.GetForUpdateAsync(x => x.Id == command.Id, relatedProperties: rp,
                          cancellationToken: cancellationToken)
                      ?? throw new ObjectNotFoundException(nameof(Invoice), nameof(Invoice.Id), command.Id);

        if (invoice.Status != InvoiceStatus.Draft)
            throw new InvalidOperationException("Invoice is not in draft status");

        invoice.Publish(command);

        var invoiceUrl = $"https://innvoicer.tianshan.space/invoice/{invoice.Key}";
        var message = $"{invoice.Company.Name} sent you an invoice. View it here: {invoiceUrl}";

        if (invoice.Client.Phone != null)
            await smsService.SendSms(message, "995", invoice.Client.Phone, cancellationToken);

        await repository.UpdateAsync(invoice, cancellationToken);
    }
}