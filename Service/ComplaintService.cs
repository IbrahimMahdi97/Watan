using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

public class ComplaintService : IComplaintService
{
    private readonly IRepositoryManager _repository;
    private readonly IConfiguration _configuration;

    public ComplaintService(IRepositoryManager repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }
    
    public async Task<IEnumerable<ComplaintDto>> GetAllComplaints()
    {
        var complaints = await _repository.Complaint.GetAllComplaints();
        return complaints;
    }

    public async Task<ComplaintDto> GetComplaintById(int id)
    {
        var complaint = await _repository.Complaint.GetComplaintById(id);
        return complaint;
    }

    public async Task<int> CreateComplaint(ComplaintForManipulationDto complaint, int userId)
    {
        var result = await _repository.Complaint.CreateComplaint(complaint, userId);
        return result;
    }

    public async Task UpdateComplaint(int id, ComplaintForManipulationDto complaint)
    {
        await _repository.Complaint.UpdateComplaint(id, complaint);
    }

    public async Task DeleteComplaint(int id)
    {
        await _repository.Complaint.DeleteComplaint(id);
    }
}