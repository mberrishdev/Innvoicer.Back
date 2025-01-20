using System.ComponentModel.DataAnnotations;
using Innvoicer.Application.Features.Companies.Queries.Models;
using Innvoicer.Domain.Entities.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innvoicer.Api.Controllers.UserController;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("v{version:apiVersion}/user")]
public class UserController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPut("change-password")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompanyModel>>> GetList([Required] UpdateUserPasswordCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = UserModel!.UserId;
        command.UserModel = UserModel;
        await Mediator.Send(command, cancellationToken);
        return Ok(UserModel!.UserId);
    }
}