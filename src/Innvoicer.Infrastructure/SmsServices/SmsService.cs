using Innvoicer.Application.Infrastructure.Contracts.SmsServices;
using Innvoicer.Infrastructure.Exceptions;
using Innvoicer.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Innvoicer.Infrastructure.SmsServices;

public class SmsService(
    IUbilApiService ubilApiService,
    ILogger<SmsService> logger,
    IOptions<SmsServiceSettings> options) : ISmsService
{
    public async Task SendBatchSms(string message, List<string> phoneNumbers, CancellationToken cancellationToken)
    {
        var key = options.Value.ApiKey;
        var brandId = options.Value.BrandId;

        var numbers = string.Join(",", phoneNumbers);

        try
        {
            var response = await ubilApiService.SendSms(key, brandId, numbers, message, false, cancellationToken);
            if (response.StatusId != 0)
            {
                throw new SmsException(response.Message);
            }
        }
        catch (Exception? ex)
        {
            throw new SmsException(ex);
        }
    }

    public async Task SendSms(string message, string dialCode, string phoneNumber, CancellationToken cancellationToken)
    {
        var key = options.Value.ApiKey;
        var brandId = options.Value.BrandId;

        try
        {
            var response = await ubilApiService.SendSms(key, brandId, $"{dialCode}{phoneNumber}", message, false,
                cancellationToken);
            if (response.StatusId != 0)
            {
                throw new SmsException(response.Message);
            }
        }
        catch (Exception? ex)
        {
            logger.LogError(ex, "Exception occured while sending sms");
            throw new SmsException(ex);
        }
    }
}