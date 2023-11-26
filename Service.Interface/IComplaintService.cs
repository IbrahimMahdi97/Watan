using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IComplaintService
{
    Task<IEnumerable<ComplaintDto>> GetAllComplaints(string? search);
    Task<ComplaintDto> GetComplaintById(int id);
    Task<int> CreateComplaint(ComplaintForManipulationDto complaint, int userId);
    Task UpdateComplaint(int id, ComplaintForManipulationDto complaint);
    Task DeleteComplaint(int id);
    Task<IEnumerable<ComplaintDto>> GetUserComplaints(int userId);
}