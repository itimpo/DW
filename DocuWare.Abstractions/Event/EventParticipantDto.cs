namespace DocuWare.Abstractions.Event;

public record EventParticipantDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
