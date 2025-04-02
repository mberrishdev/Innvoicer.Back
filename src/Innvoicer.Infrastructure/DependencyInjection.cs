using Innvoicer.Application.Infrastructure.Contracts.SmsServices;
using Innvoicer.Infrastructure.Settings;
using Innvoicer.Infrastructure.SmsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;

namespace Innvoicer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISmsService, SmsService>();
        services.Configure<SmsServiceSettings>(configuration.GetSection("SmsServiceSettings"));

        services.AddRestEaseClient<IUbilApiService>("https://api.ubill.dev");

        return services;
    }
}