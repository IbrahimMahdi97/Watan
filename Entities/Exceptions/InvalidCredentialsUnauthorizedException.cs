namespace Entities.Exceptions;

public class InvalidCredentialsUnauthorizedException : UnauthorizedException
{
    public InvalidCredentialsUnauthorizedException(string phoneNumber)
        : base($"Invalid credentials for phone number : {phoneNumber}.")
    {
    }
}