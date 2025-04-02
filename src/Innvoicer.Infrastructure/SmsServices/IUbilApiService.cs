using Innvoicer.Application.Infrastructure.Contracts.SmsServices.Models;
using RestEase;

namespace Innvoicer.Infrastructure.SmsServices;

public interface IUbilApiService
{
    [Get("v1/sms/send")]
    Task<SendSmsResponse> SendSms([Query] string key, [Query] string brandID, [Query] string numbers,
        [Query] string text,
        [Query] bool stopList, CancellationToken cancellationToken);
}