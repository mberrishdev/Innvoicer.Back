using Innvoicer.Application.Contracts.AuthServices.Models;

namespace Innvoicer.Application.Contracts.AuthServices;

public interface IAuthService
{
    Task<AuthResponse> Authorize(AuthRequest authRequest, CancellationToken cancellationToken);
}