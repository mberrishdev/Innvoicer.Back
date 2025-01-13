using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Repository.Repository;
using Innvoicer.Application.Contracts.AuthServices;
using Innvoicer.Application.Contracts.AuthServices.Models;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Features.Companies.Queries;
using Innvoicer.Application.Helpers;
using Innvoicer.Application.Settings;
using Innvoicer.Domain;
using Innvoicer.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Innvoicer.Application.Services.AuthServices;

public class AuthService(IQueryRepository<User> userRepository, IMediator mediator,
    IOptions<AuthSettings> authSettings) : IAuthService
{
    public async Task<AuthResponse> Authorize(AuthRequest authRequest, CancellationToken cancellationToken)
    {
        const string invalidCredentials = "Invalid credentials.";

        var user = await userRepository.GetAsync(
            x => x.Email.ToLower() == authRequest.Email.ToLower(),
            cancellationToken: cancellationToken);

        if (!HashHelper.Hash(authRequest.Password).Equals(user.Password))
        {
            throw new InvalidCredentialsException(invalidCredentials);
        }

        var claims = GetClaims(user);

        var secretKey = authSettings.Value.SecretKey;
        var validIssuer = authSettings.Value.ValidIssuer;
        var validAudience = authSettings.Value.ValidAudience;
        var tokenExpirationMinutes = authSettings.Value.TokenExpirationMinutes;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var jwt = new JwtSecurityToken(
            issuer: validIssuer,
            audience: validAudience,
            notBefore: DateTimeHelper.Now,
            claims: claims,
            expires: DateTimeHelper.Now.AddMinutes(tokenExpirationMinutes),
            signingCredentials: signInCred);

        var company = await mediator.Send(new ListCompanyByUserIdQuery(user.Id), cancellationToken);
        return new AuthResponse
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes),
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Companies = company,
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