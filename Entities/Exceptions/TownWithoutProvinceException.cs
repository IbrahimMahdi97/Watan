namespace Entities.Exceptions;

public class TownWithoutProvinceException : BadRequestException
{
    public TownWithoutProvinceException(int townId)
        : base($"Cannot insert town with Id: {townId}, without providing province Id")
    {
        
    }
}