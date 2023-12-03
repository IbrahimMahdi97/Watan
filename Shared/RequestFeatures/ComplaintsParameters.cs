using Entities.Enums;

namespace Shared.RequestFeatures;

public class ComplaintsParameters : RequestParameters
{
    public ComplaintStatus Status { get; set; }
    public string? Details { get; set; }
}