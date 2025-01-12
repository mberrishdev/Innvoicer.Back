using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Repository.Repository;
using Innvoicer.Application.Contracts.AuthServices;
using Innvoicer.Application.Contracts.AuthServices.Models;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Helpers;
using Innvoicer.Domain;
using Innvoicer.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Innvoicer.Application.Services.AuthServices;

public class AuthService(IQueryRepository<User> userRepository) : IAuthService
{
    public async Task<AuthResponse> Authorize(AuthRequest authRequest, CancellationToken cancellationToken)
    {
        const string invalidCredentials = "Invalid credentials.";

        var user = await userRepository.GetAsync(
            x => x.Email.Equals(authRequest.Email, StringComparison.CurrentCultureIgnoreCase),
            cancellationToken: cancellationToken);

        if (!HashHelper.Hash(authRequest.Password).Equals(user.Password))
        {
            throw new InvalidCredentialsException(invalidCredentials);
        }

        var claims = GetClaims(user);

        const string secretKey = "8AAF3A95AF6EF22591D16AB8823A9B5BE72159BCE8953DB7FA6198C29C";
        const string validIssuer = "http://localhost:4200";
        const string validAudience = "http://localhost:4200";
        const int tokenExpirationMinutes = 60;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var jwt = new JwtSecurityToken(
            issuer: validIssuer,
            audience: validAudience,
            notBefore: DateTimeHelper.Now,
            claims: claims,
            expires: DateTimeHelper.Now.AddMinutes(tokenExpirationMinutes),
            signingCredentials: signInCred);

        var randomNumber = new byte[32];

        return new AuthResponse
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes),
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
        };
    }

    private static IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var newClaim = claims.ToList();

        return newClaim;
    }
}