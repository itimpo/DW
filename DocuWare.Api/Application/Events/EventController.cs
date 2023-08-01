using DocuWare.Abstractions;
using DocuWare.Abstractions.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace DocuWare.Web.Controllers.Events;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    private readonly ILogger<EventController> _logger;

    public EventController(
        IEventRepository eventRepository,
        ILogger<EventController> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;
    }

    /// <summary>
    /// Gets events list
    /// </summary>
    /// <response code="200">Successful operation</response>
    [ProducesResponseType(typeof(EventDto[]), (int)HttpStatusCode.OK)]
    [HttpGet("")]
    public async Task<IActionResult> Events()
    {
        return Ok(await _eventRepository.GetEvents().ToArrayAsync());
    }

    /// <summary>
    /// Gets event by id
    /// </summary>
    /// <response code="200">Successful operation</response>
    [ProducesResponseType(typeof(EventDto[]), (int)HttpStatusCode.OK)]
    [HttpGet("{eventId:int}")]
    public async Task<IActionResult> Event(int eventId)
    {
        return Ok(await _eventRepository.GetEvents().FirstOrDefaultAsync(q=>q.Id == eventId));
    }

    /// <summary>
    /// Gets event participants
    /// </summary>
    /// <param name="eventId">Event Id</param>
    /// <response code="200">Successful operation</response>
    [ProducesResponseType(typeof(EventParticipantDto[]), (int)HttpStatusCode.OK)]
    [HttpGet("{eventId:int}/Participants"), Authorize]
    public async Task<IActionResult> EventParticipants(int eventId)
    {
        return Ok(await _eventRepository.GetEventParticipants(eventId).ToArrayAsync());
    }


    /// <summary>
    /// Creates new event
    /// </summary>
    /// <response code="200">Successful operation</response>
    /// <response code="400">Invalid post model</response>
    /// <response code="401">User not found</response>
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPost(""), Authorize]
    public async Task<IActionResult> EventPost([FromBody] EventDto dto)
    {
        if (dto is null)
        {
            return BadRequest(new Result { Error = "Event model is empty" });
        }

        dto.UserId = int.Parse(User.FindFirstValue(ClaimTypes.Sid)??String.Empty);
        
        var result = await _eventRepository.SaveEvent(dto);
        
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Updates event
    /// </summary>
    /// <param name="eventId">Event Id</param>
    /// <response code="200">Successful operation</response>
    /// <response code="400">Invalid post model</response>
    /// <response code="401">User not found</response>
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPut("{eventId:int}"), Authorize]
    public async Task<IActionResult> EventPut(int eventId, [FromBody] EventDto dto)
    {
        if (dto is null)
        {
            return BadRequest(new Result { Error = "Event model is empty" });
        }

        dto.Id = eventId;
        dto.UserId = int.Parse(User.FindFirstValue(ClaimTypes.Sid) ?? String.Empty);

        var result = await _eventRepository.SaveEvent(dto);

        return result.Success ? Ok(result) : BadRequest(result);
    }


    /// <summary>
    /// Creates new event participant
    /// </summary>
    /// <response code="200">Successful operation</response>
    /// <response code="400">Invalid post model</response>
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPost("{eventId:int}/Participant")]
    public async Task<IActionResult> EventParticipantPost(int eventId, [FromBody] EventParticipantDto dto)
    {
        if (dto is null)
        {
            return BadRequest(new Result { Error = "Event Participant model is empty" });
        }

        dto.EventId = eventId;

        var result = await _eventRepository.SaveEventParticipant(dto);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}