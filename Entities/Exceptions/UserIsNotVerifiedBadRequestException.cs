namespace Entities.Exceptions;

public class UserIsNotVerifiedBadRequestException : BadRequestException
{
    public UserIsNotVerifiedBadRequestException(string email)
        : base($"user with email : {email}, is not verified.. please change your password!")
    {
    }
}