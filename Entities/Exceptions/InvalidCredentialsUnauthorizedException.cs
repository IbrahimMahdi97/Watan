namespace Entities.Exceptions;

public class InvalidCredentialsUnauthorizedException : UnauthorizedException
{
    public InvalidCredentialsUnauthorizedException(string email)
        : base($"Invalid credentials for email : {email}.")
    {
    }
}