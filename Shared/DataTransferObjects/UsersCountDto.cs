namespace Shared.DataTransferObjects;

public class UsersCountDto
{
    public int TotalCount { get; set; }
    public int NewCount { get; set; }
    public int ActiveCount { get; set; }
    public int InactiveCount { get; set; }
}