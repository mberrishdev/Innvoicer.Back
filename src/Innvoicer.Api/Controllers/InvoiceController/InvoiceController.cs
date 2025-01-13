using Innvoicer.Domain.Entities.Invoices.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innvoicer.Api.Controllers.InvoiceController;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("v{version:apiVersion}/invoice")]
public class InvoiceController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost()]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<long>> Create(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        command.UserModel = UserModel!;
        return Ok(await mediator.Send(command, cancellationToken));
    }
}