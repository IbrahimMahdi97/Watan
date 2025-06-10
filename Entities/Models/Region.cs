namespace Entities.Models;

public class Region
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int TownId { get; set; }
}