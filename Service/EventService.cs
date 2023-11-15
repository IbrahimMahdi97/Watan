using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;

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

    public async Task<IEnumerable<EventWithPostDto>> GetAllEvents()
    {
        var events = await _repository.Event.GetAllEvents();
        return events;
    }

    public async Task<EventWithPostDto> GetEventById(int id)
    {
        var eventDetails = await _repository.Event.GetEventById(id);
        eventDetails.PostDetails = await _repository.Post.GetPostById(id);
        return eventDetails;
    }

    public async Task<int> Create(EventWithPostDto eventDto, int userId)
    {
        var (postId, connection, transaction) = await _repository.Post.CreatePost(eventDto.PostDetails, userId, "EVT");
        await _repository.Event.Create(eventDto, postId, connection, transaction);
        
        if (eventDto.PostDetails.PostImage is not null)
            await _fileStorageService.CopyFileToServer(postId,
                _configuration["PostImagesSetStorageUrl"]!, eventDto.PostDetails.PostImage);

        return postId;
    }

    public async Task Update(int id, EventForManiupulationDto eventDto)
    {
        //Todo: what about update title and description ?? and then image ?
        await _repository.Event.Update(id, eventDto);
    }
}