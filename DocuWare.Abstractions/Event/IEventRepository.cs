namespace DocuWare.Abstractions.Event;

public interface IEventRepository
{
    IQueryable<EventDto> GetEvents();
    IQueryable<EventParticipantDto> GetEventParticipants(int eventId);
    Task<Result> SaveEvent(EventDto dto);
    Task<Result> SaveEventParticipant(EventParticipantDto dto);
    Task<Result> DeleteEvent(int id);
}