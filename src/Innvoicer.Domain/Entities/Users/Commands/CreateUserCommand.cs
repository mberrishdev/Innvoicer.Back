using System.Text.Json.Serialization;
using FluentValidation;
using Innvoicer.Domain.Primitives;
using Innvoicer.Domain.Validators;

namespace Innvoicer.Domain.Entities.Users.Commands;

[CommandValidation(typeof(CreateUserCommandValidator))]
public class CreateUserCommand : CommandBase<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonIgnore] public string PasswordHash { get; set; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
    }
}