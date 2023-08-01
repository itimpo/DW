using DocuWare.Infrastructure.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocuWare.Infrastructure.Events;

[Table("Events")]
internal class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [StringLength(250)]
    public string Description { get; set; } = string.Empty;
    [StringLength(100)]
    public string Location { get; set; } = string.Empty;
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; } 

    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("EventId")]
    public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
}
