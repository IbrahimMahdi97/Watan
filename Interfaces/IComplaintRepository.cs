using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Interfaces;

public interface IComplaintRepository
{
    Task<IEnumerable<ComplaintDto>> GetAllComplaints(ComplaintsParameters parameters);
    Task<ComplaintDto> GetComplaintById(int id);
    Task<int> CreateComplaint(ComplaintForManipulationDto complaintDto, int userId);
    Task UpdateComplaint(int id, ComplaintForManipulationDto complaintDto);
    Task DeleteComplaint(int id);
    Task<IEnumerable<ComplaintDto>> GetUserComplaints(int userId);
}