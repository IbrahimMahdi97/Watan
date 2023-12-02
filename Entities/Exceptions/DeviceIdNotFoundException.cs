namespace Entities.Exceptions;

public class DeviceIdNotFoundException : NotFoundException
{
    public DeviceIdNotFoundException(int id)
        : base($"Device id for user with id : {id}, not found !")
    {
    }
}