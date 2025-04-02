namespace Innvoicer.Application.Infrastructure.Contracts.SmsServices;

public interface ISmsService
{
    Task SendBatchSms(string message, List<string> phoneNumbers, CancellationToken cancellationToken);
    Task SendSms(string message, string dialCode, string phoneNumbers, CancellationToken cancellationToken);
}