namespace Entities.Exceptions;

public class UserEmailAlreadyExistsBadRequestException : BadRequestException
{
    public UserEmailAlreadyExistsBadRequestException(string email)
        : base($"User email : {email} already exists!")
    {
        
    }
}