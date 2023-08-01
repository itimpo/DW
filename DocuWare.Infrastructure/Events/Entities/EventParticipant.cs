using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocuWare.Infrastructure.Events;

[Table("EventParticipants")]
internal class EventParticipant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int EventId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;
    [StringLength(100)]
    public string EmailAddress { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Event? Event { get; set; }
}
