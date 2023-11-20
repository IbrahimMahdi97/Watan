namespace Shared.RequestFeatures;

public class EventsParameters : RequestParameters
{
    public DateTime AddedFromDate { get; set; } = new(1753, 01, 01); //SQL Server minimum datetime value
    public DateTime AddedToDate { get; set; } = DateTime.Now;
    public DateTime FromEventDate { get; set; } = new(1753, 01, 01); //SQL Server minimum datetime value
    public DateTime ToEventDate { get; set; } = DateTime.Now.AddYears(1);
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public int AddedByUserId { get; set; }
}