using Shared.DataTransferObjects;

namespace Interfaces;

public interface IComplaintRepository
{
    Task<IEnumerable<ComplaintDto>> GetAllComplaints();
    Task<ComplaintDto> GetComplaintById(int id);
    Task<int> CreateComplaint(ComplaintForManipulationDto complaintDto, int userId);
    Task UpdateComplaint(int id, ComplaintForManipulationDto complaintDto);
    Task DeleteComplaint(int id);
}