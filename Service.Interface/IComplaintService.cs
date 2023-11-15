using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IComplaintService
{
    Task<IEnumerable<ComplaintDto>> GetAllComplaints();
    Task<ComplaintDto> GetComplaintById(int id);
    Task<int> CreateComplaint(ComplaintForManipulationDto complaint, int userId);
    Task UpdateComplaint(int id, ComplaintForManipulationDto complaint);
    Task DeleteComplaint(int id);
}