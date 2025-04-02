namespace Innvoicer.Application.Infrastructure.Contracts.SmsServices.Models;

public class SendSmsResponse
{
    public int StatusId { get; set; }
    public string Message { get; set; }
}

public enum SendSmsStatus
{
    SMSSended = 0,
    BrandIDNotFound = 10,
    NumbersNotFound = 20,
    EmptyMessageText = 30,
    NotEnoughSMS = 40,
    ValidNumbersNotFound = 50,
    JsonError = 90,
    GeneralError = 90,
}