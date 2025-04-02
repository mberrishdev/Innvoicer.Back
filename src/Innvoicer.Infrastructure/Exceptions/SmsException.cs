namespace Innvoicer.Infrastructure.Exceptions;

public class SmsException : Exception
{
    public SmsException(string statusMessage) : base($"SMS send failed. Status {statusMessage}")
    {
    }

    public SmsException(Exception? exception) : base(GetExceptionMessages(exception))
    {
    }

    private static string GetExceptionMessages(Exception? e, string msgs = "")
    {
        if (e == null)
            return string.Empty;

        if (msgs == "")
            msgs = e.Message;

        if (e.InnerException != null)
            msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);

        return msgs;
    }
}