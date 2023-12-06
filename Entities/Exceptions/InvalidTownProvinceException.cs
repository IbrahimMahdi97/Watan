namespace Entities.Exceptions;

public class InvalidTownProvinceException : BadRequestException
{
    public InvalidTownProvinceException(int townId, int provinceId)
        : base($"Town with Id {townId} does not belong to province with Id {provinceId}.")
    {
        
    }
}