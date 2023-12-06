namespace Entities.Exceptions;

public class StringLimitExceededBadRequestException : BadRequestException
{
    public StringLimitExceededBadRequestException(string field, int length)
        : base($"The provided value for the field: {field},\n" +
               $" has exceeded the length limit which is {length} characters !")
    {
    }
}