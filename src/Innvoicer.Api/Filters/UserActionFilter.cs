using System.Security.Claims;
using Innvoicer.Api.Controllers;
using Innvoicer.Domain.Primitives;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Innvoicer.Api.Filters;

public class UserActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is ApiControllerBase c)
        {
            var userModel = new UserModel
            {
                Email = c.User?.FindFirstValue(ClaimTypes.Email)
            };

            _ = long.TryParse(c.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            userModel.UserId = userId;
            c.UserModel = userModel;
        }

        base.OnActionExecuting(context);
    }
}