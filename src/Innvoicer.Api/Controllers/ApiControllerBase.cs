using Innvoicer.Api.Filters;
using Innvoicer.Domain.Primitives;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Innvoicer.Api.Controllers;

/// <summary>
/// Base Controller
/// </summary>
[ApiController]
[UserActionFilter]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// IMediator
    /// </summary>
    protected readonly IMediator Mediator;

    public UserModel? UserModel { get; set; } = null;

    /// <summary>
    /// ApiControllerBase Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}