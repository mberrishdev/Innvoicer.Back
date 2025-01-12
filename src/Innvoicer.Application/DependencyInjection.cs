using System.Reflection;
using Innvoicer.Application.Contracts.AuthServices;
using Innvoicer.Application.Services.AuthServices;
using Innvoicer.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Innvoicer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));

        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}