namespace Entities.Exceptions;

public class InvalidCredentialsEmailUnauthorizedException : UnauthorizedException
{
    public InvalidCredentialsEmailUnauthorizedException(string email)
        : base($"Invalid credentials for email : {email}.")
    {
    }
}