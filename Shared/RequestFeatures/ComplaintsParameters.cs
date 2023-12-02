namespace Shared.RequestFeatures;

public class ComplaintsParameters : RequestParameters
{
    public int TypeId { get; set; }
    public string? Details { get; set; }
}