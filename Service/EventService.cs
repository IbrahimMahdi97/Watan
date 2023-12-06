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
        if (eventDto.ProvinceId > 0) await IsProvinceExist(eventDto.ProvinceId);
        if (eventDto.TownId > 0) await IsTownExist(eventDto.TownId);
        
        var (postId, connection, transaction) = await _repository.Post.CreatePost(eventDto.PostDetails, userId, "EVT");
        await _repository.Event.Create(eventDto, postId, connection, transaction);
        
        if (eventDto.PostDetails.PostImage is not null)
            await _fileStorageService.CopyFileToServer(postId,
                _configuration["PostImagesSetStorageUrl"]!, eventDto.PostDetails.PostImage);

        return postId;
    }

    public async Task Update(int id, EventWithPostForCreationDto eventDto)
    {
        if (eventDto.ProvinceId > 0) await IsProvinceExist(eventDto.ProvinceId);
        if (eventDto.TownId > 0) await IsTownExist(eventDto.TownId);
        
        await _repository.Post.UpdatePost(id, eventDto.PostDetails);
        await _repository.Event.Update(id, eventDto);
    }

    private async Task IsProvinceExist(int provinceId)
    {
        var provinceService = new ProvinceService(_repository);
        await provinceService.GetById(provinceId);
    }

    private async Task IsTownExist(int townId, int? provinceId = null)
    {
        var townService = new TownService(_repository);
        var town = await townService.GetById(townId);
        if (provinceId.HasValue && town.ProvinceId != provinceId.Value)
        {
            throw new InvalidTownProvinceException(townId, provinceId.Value);
        }
    }

}