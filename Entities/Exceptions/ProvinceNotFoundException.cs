namespace Entities.Exceptions;

public class ProvinceNotFoundException : NotFoundException
{
    public ProvinceNotFoundException(int provinceId)
        : base($"The province with id: {provinceId} doesn't exist in the database.")
    {
    }
}