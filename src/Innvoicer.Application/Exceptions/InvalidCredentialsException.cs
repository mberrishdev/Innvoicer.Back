namespace Innvoicer.Application.Exceptions;

public class InvalidCredentialsException : ApplicationException
{
    public InvalidCredentialsException(string message) : base(message)
    {
    }
}