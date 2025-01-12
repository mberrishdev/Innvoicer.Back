using System.Threading;
using System.Threading.Tasks;
using Innvoicer.Application.Contracts.AuthServices;
using Innvoicer.Application.Contracts.AuthServices.Models;
using Innvoicer.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Innvoicer.Api.Controllers.AuthController;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/auth")]
public class AuthController(IMediator mediator, IAuthService authService) : ApiControllerBase(mediator)
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest authRequest,
        CancellationToken cancellationToken)
    {
        var result = await authService.Authorize(authRequest, cancellationToken);
        return Ok(result);
    }

    [HttpGet("hash")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public ActionResult<AuthResponse> GetHash([FromQuery] string str)
    {
        return Ok(HashHelper.Hash(str));
    }
}