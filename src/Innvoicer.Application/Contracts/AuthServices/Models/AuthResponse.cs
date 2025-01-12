using System;

namespace Innvoicer.Application.Contracts.AuthServices.Models;

public class AuthResponse
{
    public long UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime Expires { get; set; }
    public required string Token { get; set; }
}