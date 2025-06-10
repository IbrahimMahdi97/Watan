namespace Entities.Exceptions;

public class InvalidCredentialsEmailOrPhoneNumberUnauthorizedException : UnauthorizedException
{
    public InvalidCredentialsEmailOrPhoneNumberUnauthorizedException(string emailOrPhoneNumber)
        : base($"Invalid credentials for Email or Phone number : {emailOrPhoneNumber}.")
    {
    }
}