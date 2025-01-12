namespace Innvoicer.Domain;

public static class DateTimeHelper
{
    public static DateTime Now
    {
        get
        {
            var gmtPlus4 = TimeZoneInfo.FindSystemTimeZoneById("Georgian Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus4);
        }
    }
}