using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interface;

public interface IComplaintService
{
    Task<IEnumerable<ComplaintDto>> GetAllComplaints(ComplaintsParameters parameters);
    Task<ComplaintDto> GetComplaintById(int id);
    Task<int> CreateComplaint(ComplaintForManipulationDto complaint, int userId);
    Task UpdateComplaint(int id, ComplaintForUpdateDto complaint);
    Task DeleteComplaint(int id);
    Task<MyComplaintsDto> GetUserComplaints(int userId, ComplaintsParameters parameters);
}