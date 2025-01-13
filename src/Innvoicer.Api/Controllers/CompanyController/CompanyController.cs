using Innvoicer.Application.Features.Companies.Queries;
using Innvoicer.Application.Features.Companies.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innvoicer.Api.Controllers.CompanyController;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("v{version:apiVersion}/company")]
public class CompanyController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet()]
    [ProducesResponseType(typeof(List<CompanyModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompanyModel>>> GetList(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new ListCompanyByUserIdQuery(UserModel!.UserId), cancellationToken));
    }
}