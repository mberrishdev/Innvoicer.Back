using System.ComponentModel.DataAnnotations;
using Innvoicer.Application.Features.Invoices.Queries;
using Innvoicer.Application.Features.Invoices.Queries.Models;
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
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<long>> Update([Required, FromRoute] long id, UpdateInvoiceCommand command,
        CancellationToken cancellationToken)
    {
        command.UserModel = UserModel!;
        command.Id = id;
        await Mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpGet("list/{companyId:long}")]
    [ProducesResponseType(typeof(List<InvoiceModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<InvoiceModel>>> GetList([Required, FromRoute] long companyId,
        CancellationToken cancellationToken)
    {
        var query = new ListInvoicesQuery(companyId);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(InvoiceModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<InvoiceModel>> Get([Required, FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var query = new GetInvoicesByIdQuery(id);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("key/{key}")]
    [ProducesResponseType(typeof(InvoiceModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<InvoiceModel>> Get([Required, FromRoute] string key,
        CancellationToken cancellationToken)
    {
        var query = new GetInvoicesByKeyQuery(key);
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}