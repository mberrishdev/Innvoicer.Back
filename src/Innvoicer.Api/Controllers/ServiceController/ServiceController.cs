using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Innvoicer.Application.Contracts.AuthServices;
using Innvoicer.Application.Contracts.AuthServices.Models;
using Innvoicer.Application.Features.Companies.Queries;
using Innvoicer.Application.Features.Companies.Queries.Models;
using Innvoicer.Application.Features.Services.Queries;
using Innvoicer.Application.Features.Services.Queries.Models;
using Innvoicer.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Innvoicer.Api.Controllers.ServiceController;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("v{version:apiVersion}/service")]
public class ServiceController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet("{companyId:long}")]
    [ProducesResponseType(typeof(List<ServiceModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompanyModel>>> GetList(long companyId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new ListServiceByCompanyIdQuery(companyId), cancellationToken));
    }
}