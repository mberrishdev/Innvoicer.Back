using System.Text.Json.Serialization;
using MediatR;

namespace Innvoicer.Domain.Primitives;

public class CommandBase<TResponse> : CommandBaseValidator, IRequest<TResponse>
{
    [JsonIgnore] public UserModel? UserModel { get; set; }
}

public class CommandBase : CommandBaseValidator, IRequest
{
    [JsonIgnore] public UserModel? UserModel { get; set; }
}