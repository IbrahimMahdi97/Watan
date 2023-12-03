using Entities.Enums;
using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class ComplaintService : IComplaintService
{
    private readonly IRepositoryManager _repository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;
    
    public ComplaintService(IRepositoryManager repository, IFileStorageService fileStorageService, IConfiguration configuration)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
        _configuration = configuration;
    }
    
    public async Task<IEnumerable<ComplaintDto>> GetAllComplaints(ComplaintsParameters parameters)
    {
        var complaints = await _repository.Complaint.GetAllComplaints(parameters);
        var complaintsDto = complaints.ToList();
        foreach (var complaint in complaintsDto)
        {
            var images = _fileStorageService.GetFilesUrlsFromServer(
                complaint.Id,
                _configuration["ComplaintImagesSetStorageUrl"]!,
                _configuration["ComplaintImagesGetStorageUrl"]!
            ).ToList();

            complaint.ImageUrl = images.Any() ? images.First() : string.Empty;
        }

        return complaintsDto;
    }

    public async Task<ComplaintDto> GetComplaintById(int id)
    {
        var complaint = await _repository.Complaint.GetComplaintById(id);
        var images = _fileStorageService.GetFilesUrlsFromServer(
            complaint.Id,
            _configuration["ComplaintImagesSetStorageUrl"]!,
            _configuration["ComplaintImagesGetStorageUrl"]!
        ).ToList();

        complaint.ImageUrl = images.Any() ? images.First() : string.Empty;
        
        return complaint;
    }

    public async Task<int> CreateComplaint(ComplaintForManipulationDto complaint, int userId)
    {
        var result = await _repository.Complaint.CreateComplaint(complaint, userId);
        if (complaint.ComplaintImage is not null)
            await _fileStorageService.CopyFileToServer(result,
                _configuration["ComplaintImagesSetStorageUrl"]!, complaint.ComplaintImage);
        return result;
    }

    public async Task UpdateComplaint(int id, ComplaintForUpdateDto complaint)
    {
        await _repository.Complaint.UpdateComplaint(id, complaint);
        if (complaint.ComplaintImage is not null)
            await _fileStorageService.CopyFileToServer(id,
                _configuration["ComplaintImagesSetStorageUrl"]!, complaint.ComplaintImage);
    }

    public async Task DeleteComplaint(int id)
    {
        await _repository.Complaint.DeleteComplaint(id);
    }

    public async Task<MyComplaintsDto> GetUserComplaints(int userId, ComplaintsParameters parameters)
    {
        var complaints = await _repository.Complaint.GetUserComplaints(userId, parameters);
        var complaintsDto = complaints.ToList();
        foreach (var complaint in complaintsDto)
        {
            var images = _fileStorageService.GetFilesUrlsFromServer(
                complaint.Id,
                _configuration["ComplaintImagesSetStorageUrl"]!,
                _configuration["ComplaintImagesGetStorageUrl"]!
            ).ToList();

            complaint.ImageUrl = images.Any() ? images.First() : string.Empty;
        }

        return new MyComplaintsDto
        {
            OnHoldComplaintsCount = complaintsDto.Count(c => c.Status == ComplaintStatus.OnHold),
            CancelledComplaintsCount = complaintsDto.Count(c => c.Status == ComplaintStatus.Cancelled),
            DoneComplaintsCount = complaintsDto.Count(c => c.Status == ComplaintStatus.Done),
            Complaints = complaintsDto.Where(c => c.Status == parameters.Status)
        };
    }
}