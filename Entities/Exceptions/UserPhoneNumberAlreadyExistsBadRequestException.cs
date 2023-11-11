namespace Entities.Exceptions;

public class UserPhoneNumberAlreadyExistsBadRequestException : BadRequestException
{
    public UserPhoneNumberAlreadyExistsBadRequestException(string phoneNumber)
        : base($"User phone number : {phoneNumber} already exists!")
    {
        
    }
}