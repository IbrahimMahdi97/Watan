namespace Entities.Exceptions;

public class NoCredentialsUnauthorizedException : UnauthorizedException
{
    public NoCredentialsUnauthorizedException()
        : base("Either phone number or email must be provided.")

    {
    }
}