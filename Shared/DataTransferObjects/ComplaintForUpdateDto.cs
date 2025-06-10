using Entities.Enums;

namespace Shared.DataTransferObjects;

public class ComplaintForUpdateDto : ComplaintForManipulationDto
{
  public ComplaintStatus Status { get; set; }

}