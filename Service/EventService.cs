using Entities.Exceptions;
using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class EventService : IEventService
{
    private readonly IRepositoryManager _repository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;

    public EventService(IRepositoryManager repository, IFileStorageService fileStorageService, IConfiguration configuration)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
        _configuration = configuration;
    }

    public async Task<PagedList<EventWithPostDto>> GetAllEvents(EventsParameters eventsParameters)
    {
        var events = await _repository.Event.GetAllEvents(eventsParameters);
        foreach (var eventDetails in events)
        {
            eventDetails.PostDetails = new PostDto
            {
                Id = eventDetails.Id,
                Description = eventDetails.Description,
                Title = eventDetails.Title,
                RecordDate = eventDetails.RecordDate
            };
            var images = _fileStorageService.GetFilesUrlsFromServer(
                    eventDetails.PostDetails.Id,
                    _configuration["PostImagesSetStorageUrl"]!,
                    _configuration["PostImagesGetStorageUrl"]!
                ).ToList();

                eventDetails.PostDetails.ImageUrl = images.Any() ? images.First() : string.Empty;
        }
        
        return events;
    }

    public async Task<EventWithPostDto> GetEventById(int id, int userId)
    {
        var eventDetails = await _repository.Event.GetEventById(id);
        eventDetails.PostDetails = await _repository.Post.GetPostById(id, userId);
        var images = _fileStorageService.GetFilesUrlsFromServer(eventDetails.PostDetails.Id,
            _configuration["PostImagesSetStorageUrl"]!,
            _configuration["PostImagesGetStorageUrl"]!).ToList();

        eventDetails.PostDetails.ImageUrl = images.Any() ? images.First() : "";
        return eventDetails;
    }

    public async Task<int> Create(EventWithPostForCreationDto eventDto, int userId)
    {
        await TownProvinceExist(eventDto.TownId, eventDto.ProvinceId);
        
        if (eventDto.PostDetails.Title is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("Title", 50);
        
        if (eventDto.Type is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("Type", 50);

        var (postId, connection, transaction) = await _repository.Post.CreatePost(eventDto.PostDetails, userId, "EVT");
        await _repository.Event.Create(eventDto, postId, connection, transaction);
        
        if (eventDto.PostDetails.PostImage is not null)
            await _fileStorageService.CopyFileToServer(postId,
                _configuration["PostImagesSetStorageUrl"]!, eventDto.PostDetails.PostImage);

        return postId;
    }

    public async Task Update(int id, EventWithPostForCreationDto eventDto)
    {
        await TownProvinceExist(eventDto.TownId, eventDto.ProvinceId);
        var post = await _repository.Post.GetPostById(id, 0);
        if (post is null) throw new PostNotFoundException(id);
        await _repository.Post.UpdatePost(id, eventDto.PostDetails);
        await _repository.Event.Update(id, eventDto);
    }

    public async Task<IEnumerable<AttendeesCountDto>> GetAttendeesCountByProvinceIdAndTownId(int provinceId, int townId)
    {
        var count = await _repository.Event.GetAttendeesCountByProvinceIdAndTownId(provinceId, townId);
        return count;
    }

    public async Task<IEnumerable<EventDto>> GetFromDateToDate(DateTime fromDate, DateTime toDate)
    {
        var events = await _repository.Event.GetFromDateToDate(fromDate, toDate);
        return events;
    }

    private async Task TownProvinceExist(int townId, int provinceId)
    {
        var town = await _repository.Town.GetById(townId);
        if (town is null) throw new TownNotFoundException(townId);
        if (town.ProvinceId != provinceId) throw new InvalidTownProvinceException(townId, provinceId);
        var province = await _repository.Province.GetProvinceById(provinceId);
        if (province is null) throw new ProvinceNotFoundException(provinceId);
    }
}