namespace Shared.DataTransferObjects;

public class NotificationForCreationDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
}