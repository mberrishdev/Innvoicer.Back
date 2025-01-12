using System.ComponentModel.DataAnnotations;

namespace Innvoicer.Application.Contracts.AuthServices.Models;

public class AuthRequest
{
    [Required] public required string Email { get; set; }
    [Required] public required string Password { get; set; }
}