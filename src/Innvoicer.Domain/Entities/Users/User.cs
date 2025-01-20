using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Innvoicer.Domain.Companies;
using Innvoicer.Domain.Entities.Users.Commands;
using Innvoicer.Domain.Primitives;

namespace Innvoicer.Domain.Entities.Users;

public class User : Entity<long>
{
    [Required, MaxLength(50)] public string FirstName { get; private set; }
    [Required, MaxLength(50)] public string LastName { get; private set; }
    [EmailAddress, MaxLength(100)] public string Email { get; private set; }
    [EmailAddress, MaxLength(300)] public string Password { get; private set; }

    [Required] public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public ICollection<Company>
        Companies { get; private set; }

    private User()
    {
    }

    public User(CreateUserCommand command)
    {
        command.Validate();

        FirstName = command.FirstName;
        LastName = command.LastName;
        Email = command.Email;
        Password = command.PasswordHash;

        CreatedAt = DateTimeHelper.Now;
    }

    public void UpdatePassword(UpdateUserPasswordCommand command)
    {
        command.Validate();

        Password = command.NewPasswordHash;
        UpdatedAt = DateTimeHelper.Now;
    }
}