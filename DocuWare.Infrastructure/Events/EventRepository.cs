using DocuWare.Abstractions;
using DocuWare.Abstractions.Event;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Events;

internal class EventRepository : IEventRepository
{
    private DwDbContext _db;
    public EventRepository(DwDbContext db)
    {
        _db = db;
    }

    public IQueryable<EventDto> GetEvents()
    {
        return _db.Events
            .OrderBy(e => e.Name)
            .Select(q => new EventDto
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                Location = q.Location,
                StartTime = q.StartTime,
                EndTime = q.EndTime,
                UserId = q.UserId
            });
    }

    public IQueryable<EventParticipantDto> GetEventParticipants(int eventId)
    {
        return _db.EventParticipants
            .Where(q => q.EventId == eventId)
            .OrderBy(q => q.Name)
            .Select(q => new EventParticipantDto
            {
                Id = q.Id,
                Name = q.Name,
                PhoneNumber = q.PhoneNumber,
                EmailAddress = q.EmailAddress,
                EventId = q.EventId,
                CreatedDate = q.CreatedDate
            });
    }

    public async Task<Result> SaveEvent(EventDto dto)
    {
        //check if event exists or create
        var evnt = await _db.Events.FirstOrDefaultAsync(q => q.Id == dto.Id);
        if (evnt == null)
        {
            evnt = new Event
            {
                UserId = dto.UserId
            };
            _db.Events.Add(evnt);
        }

        evnt.Name = dto.Name;
        evnt.Description = dto.Description;
        evnt.Location = dto.Location;
        evnt.StartTime = dto.StartTime;
        evnt.EndTime = dto.EndTime;

        try
        {
            await _db.SaveChangesAsync();

            return new Result { Success = true };
        }
        catch (Exception ex)
        {
            return new Result { Error = ex.ToString() };
        }
    }

    public async Task<Result> SaveEventParticipant(EventParticipantDto dto)
    {
        //check if event participant exists or create
        var participant = await _db.EventParticipants.FirstOrDefaultAsync(q => q.Id == dto.Id || (q.EventId == dto.EventId && q.EmailAddress == dto.EmailAddress));
        if (participant == null)
        {
            participant = new EventParticipant
            {
                EventId = dto.EventId,
                CreatedDate = DateTime.Now,
            };
            _db.EventParticipants.Add(participant);
        }

        participant.Name = dto.Name;
        participant.PhoneNumber = dto.PhoneNumber;
        participant.EmailAddress = dto.EmailAddress;

        try
        {
            await _db.SaveChangesAsync();

            return new Result { Success = true };
        }
        catch (Exception ex)
        {
            return new Result { Error = ex.ToString() };
        }
    }

    public async Task<Result> DeleteEvent(int id)
    {
        var evnt = _db.Events.FirstOrDefault(q => q.Id == id);
        if (evnt == null)
        {
            return new Result { Error = "Event not found" };
        }

        _db.Events.Remove(evnt);
        try
        {
            await _db.SaveChangesAsync();

            return new Result { Success = true };
        }
        catch (Exception ex)
        {
            return new Result { Error = ex.ToString() };
        }
    }
}
