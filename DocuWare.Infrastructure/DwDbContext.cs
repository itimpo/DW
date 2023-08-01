using Microsoft.EntityFrameworkCore;
using DocuWare.Infrastructure.Users;
using DocuWare.Infrastructure.Events;

namespace DocuWare.Infrastructure;

internal class DwDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<EventParticipant> EventParticipants { get; set; }

    public DwDbContext(DbContextOptions<DwDbContext> options)
       : base(options)
    {
        Database.EnsureCreated();
    }
}
