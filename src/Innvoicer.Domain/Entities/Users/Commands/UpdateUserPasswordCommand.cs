using System.Text.Json.Serialization;
using FluentValidation;
using Innvoicer.Domain.Primitives;
using Innvoicer.Domain.Validators;

namespace Innvoicer.Domain.Entities.Users.Commands;

[CommandValidation(typeof(UpdateUserPasswordCommandValidator))]
public class UpdateUserPasswordCommand : CommandBase
{
    [JsonIgnore] public long Id { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
    [JsonIgnore] public string? NewPasswordHash { get; set; }
}

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
    }
}